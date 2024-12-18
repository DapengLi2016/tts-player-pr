//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.TTSPlayerManagementClient;

using Microsoft.Speech.TTSPlayer.HttpClient;
using Microsoft.SpeechServices.Common;
using Microsoft.SpeechServices.CommonLib.Public.Interface;
using Microsoft.SpeechServices.CommonLib;
using Microsoft.SpeechServices.Cris.Http;
using Microsoft.SpeechServices.TTSPlayerLib;
using Microsoft.SpeechServices.VideoTranslation;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class AppHandler
{
    public static async Task<int> DoRunAndReturnExitCodeAsync(OptionsBase options, ITTSPlayerRegionConfig regionConfig)
    {
        ArgumentNullException.ThrowIfNull(options);
        var httpConfig = new TTSPlayerHttpClientConfig(
            regionConfig: regionConfig,
            subKey: options.SubscriptionKey);
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
                        CommonPublicConst.Json.WriterSettings));
                    break;
                }

            case GetPlayerOptions getOptions:
                {
                    var player = await translationClient.GetTTSPlayerAsync(getOptions.Id).ConfigureAwait(false);

                    Console.WriteLine("Get TTS player:");
                    Console.WriteLine(JsonConvert.SerializeObject(
                        player,
                        Formatting.Indented,
                        CommonPublicConst.Json.WriterSettings));
                    break;
                }

            case DeletePlayerOptions deleteOptions:
                {
                    await translationClient.DeleteTTSPlayerAsync(deleteOptions.Id).ConfigureAwait(false);

                    Console.WriteLine($"Deleted TTS player with ID: {deleteOptions.Id}");
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
                    config.Properties.ParseKind = TTSWebPagePlayerContentParseKind.WithXPathsFromHTML;

                    var player = await translationClient.CreateTTSPlayerAsync(config).ConfigureAwait(false);

                    Console.WriteLine(JsonConvert.SerializeObject(
                        player,
                        Formatting.Indented,
                        CommonPublicConst.Json.WriterSettings));

                    player = await translationClient.QueryTaskByIdUntilTerminatedAsync<TTSWebPagePlayer>(
                        id: Guid.Parse(player.Id),
                        additionalHeaders: null,
                        printFirstQueryResult: false).ConfigureAwait(false);
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
                        return CommonPublicConst.ExistCodes.GenericError;
                    }

                    Console.WriteLine($"Creating demo for player:");
                    Console.WriteLine(JsonConvert.SerializeObject(
                        player,
                        Formatting.Indented,
                        CommonPublicConst.Json.WriterSettings));

                    if (!(player.Properties.AllowedVoiceNameList?.Contains(config.VoiceName) ?? false))
                    {
                        Console.WriteLine($"Invalid voice name: {config.VoiceName}");
                        return CommonPublicConst.ExistCodes.GenericError;
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
                            return CommonPublicConst.ExistCodes.GenericError;
                        }
                    }

                    await JavascriptDemoHelper.GenerateJavascriptClientDemoHelperAsync(
                        playerId: config.PlayerId,
                        hostName: regionConfig.HostName,
                        sourceLocation: config.ContentSourceLocation,
                        voice: config.VoiceName,
                        style: config.VoiceStyle,
                        xPaths: config.HtmlXPathList,
                        targetDir: createDemoOptions.TargetDir).ConfigureAwait(false);
                    Console.WriteLine($"Player created in:");
                    Console.WriteLine($"\t{createDemoOptions.TargetDir}");

                    Console.WriteLine($"Please try player from this file:");
                    Console.WriteLine($"\t{Path.Combine(createDemoOptions.TargetDir, @"client-sample\flat.html")}");
                    break;
                }

            case SynthesisMetadataOptions synthesisMetadataOptions:
                {
                    if (!File.Exists(synthesisMetadataOptions.Config))
                    {
                        throw new FileNotFoundException(synthesisMetadataOptions.Config);
                    }

                    var content = await File.ReadAllTextAsync(
                        synthesisMetadataOptions.Config,
                        Encoding.UTF8).ConfigureAwait(false);
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        throw new ArgumentNullException(nameof(content));
                    }

                    var config = JsonConvert.DeserializeObject<ClientDemoConfig>(content);
                    ArgumentNullException.ThrowIfNull(config);

                    var synthesisMetadata = await translationClient.SynthesisMetadataAsync(
                        playerId: config.PlayerId,
                        sourceLocation: config.ContentSourceLocation,
                        voice: config.VoiceName,
                        xpaths: config.HtmlXPathList,
                        style: config.VoiceStyle).ConfigureAwait(false);
                    Console.WriteLine($"Succesfully sythesized metadata:");
                    Console.WriteLine(JsonConvert.SerializeObject(
                        synthesisMetadata,
                        Formatting.Indented,
                        CommonPublicConst.Json.WriterSettings));
                    break;
                }

            default:
                throw new NotSupportedException(options.ToString());
        }

        return 0;
    }
}
