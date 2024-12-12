//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.TTSPlayerManagementClient;

using CommandLine;
using CommandLine.Text;
using Microsoft.Speech.TTSPlayer.HttpClient;
using Microsoft.SpeechServices.CommonLib;
using Microsoft.SpeechServices.Cris.Http;
using Microsoft.SpeechServices.TTSPlayerLib;
using Microsoft.SpeechServices.VideoTranslation;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        var types = LoadVerbs();

        var exitCode = await Parser.Default.ParseArguments(args, types)
            .MapResult(
                options  => RunAndReturnExitCodeAsync(options),
                _ => Task.FromResult(1));

        if (exitCode == 0)
        {
            Console.WriteLine("Success");
        }
        else
        {
            Console.WriteLine($"Failure with exit code: {exitCode}");
        }

        return exitCode;
    }

    static async Task<int> RunAndReturnExitCodeAsync(object options)
    {
        var optionsBase = options as OptionsBase;
        ArgumentNullException.ThrowIfNull(optionsBase);

        var httpConfig = new TTSPlayerHttpClientConfig(
            optionsBase.Environment,
            optionsBase.SubscriptionKey);
        var translationClient = new TTSPlayerClient(httpConfig);

        switch (options)
        {
            case ListPlayersOptions listOptions:
                {
                    var players = await translationClient.GetTTSPlayersAsync().ConfigureAwait(false);

                    Console.WriteLine("Get TTS players:");
                    Console.WriteLine(JsonConvert.SerializeObject(
                        players,
                        Formatting.Indented,
                        CustomContractResolver.WriterSettings));
                    break;
                }

            case GetPlayerOptions getOptions:
                {
                    var player = await translationClient.GetTTSPlayerAsync(getOptions.Id).ConfigureAwait(false);

                    Console.WriteLine("Get TTS player:");
                    Console.WriteLine(JsonConvert.SerializeObject(
                        player,
                        Formatting.Indented,
                        CustomContractResolver.WriterSettings));
                    break;
                }

            case DeletePlayerOptions deleteOptions:
                {
                    await translationClient.DeleteTTSPlayerAsync(deleteOptions.Id).ConfigureAwait(false);

                    Console.WriteLine($"Deleted TTS player with ID: {deleteOptions.Id}");
                    break;
                }

            case CreateDemoOptions createDemoOptions:
                {
                    if (!File.Exists(createDemoOptions.Config))
                    {
                        throw new FileNotFoundException(createDemoOptions.Config);
                    }

                    var content = await File.ReadAllTextAsync(
                        createDemoOptions.Config,
                        Encoding.UTF8).ConfigureAwait(false);
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        throw new ArgumentNullException(nameof(content));
                    }

                    var config = JsonConvert.DeserializeObject<ClientDemoConfig>(content);
                    ArgumentNullException.ThrowIfNull(config);

                    var player = await translationClient.GetTTSPlayerAsync(config.PlayerId).ConfigureAwait(false);
                    if (player == null)
                    {
                        Console.WriteLine($"Invalid player ID: {config.PlayerId}");
                        return CommonConst.ExistCodes.GenericError;

                    }

                    Console.WriteLine($"Creating demo for player:");
                    Console.WriteLine(JsonConvert.SerializeObject(
                        player,
                        Formatting.Indented,
                        CustomContractResolver.WriterSettings));

                    if (!(player.Properties.AllowedVoiceNameList?.Contains(config.VoiceName) ?? false))
                    {
                        Console.WriteLine($"Invalid voice name: {config.VoiceName}");
                        return CommonConst.ExistCodes.GenericError;
                    }

                    if ((config.HtmlXPathList?.Count ?? 0) == 0)
                    {
                        throw new ArgumentException($"{nameof(config.HtmlXPathList)} should not be empty.");
                    }

                    foreach (var xPath in config.HtmlXPathList)
                    {
                        if (!(player.Properties.AllowedHtmlXPathList?.Contains(xPath) ?? false))
                        {
                            Console.WriteLine($"Invalid xPath: {xPath}");
                            return CommonConst.ExistCodes.GenericError;
                        }
                    }

                    await JavascriptDemoHelper.GenerateJavascriptClientDemoHelperAsync(
                        playerId: config.PlayerId,
                        region: createDemoOptions.Region,
                        sourceLocation: config.ContentSourceLocation,
                        voice: config.VoiceName,
                        xPaths: config.HtmlXPathList,
                        targetDir: createDemoOptions.TargetDir).ConfigureAwait(false);
                    Console.WriteLine($"Player created in:");
                    Console.WriteLine($"\t{createDemoOptions.TargetDir}");

                    Console.WriteLine($"Please try player from this file:");
                    Console.WriteLine($"\t{Path.Combine(createDemoOptions.TargetDir, @"client-sample\flat.html")}");
                    break;
                }

            case CreatePlayerOptions createPlayerOptions:
                {
                    if (!File.Exists(createPlayerOptions.Config))
                    {
                        throw new FileNotFoundException(createPlayerOptions.Config);
                    }

                    var content = await File.ReadAllTextAsync(
                        createPlayerOptions.Config,
                        Encoding.UTF8).ConfigureAwait(false);
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        throw new ArgumentNullException(nameof(content));
                    }

                    var config = JsonConvert.DeserializeObject<TTSWebPagePlayerCreate>(content);
                    var player = await translationClient.CreateTTSPlayerAsync(config).ConfigureAwait(false);

                    Console.WriteLine(JsonConvert.SerializeObject(
                        player,
                        Formatting.Indented,
                        CustomContractResolver.WriterSettings));

                    player = await translationClient.QueryTaskByIdUntilTerminatedAsync<TTSWebPagePlayer>(
                        id: Guid.Parse(player.Id),
                        additionalHeaders: null,
                        printFirstQueryResult: false).ConfigureAwait(false);
                    break;
                }
            default:
                throw new NotSupportedException(options.ToString());
        }

        return 0;
    }

    //load all types using Reflection
    private static Type[] LoadVerbs()
    {
        return Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetCustomAttribute<VerbAttribute>() != null).ToArray();
    }
}