//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.Speech.TTSPlayer.HttpClient;

using Flurl;
using Flurl.Http;
using Microsoft.SpeechServices.CommonLib;
using Microsoft.SpeechServices.CommonLib.Util;
using Microsoft.SpeechServices.Cris.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TTSPlayerClient : HttpClientBase
{
    public TTSPlayerClient(HttpClientConfigBase config)
        : base(config)
    {
    }

    public override bool IsVersionInSegment => true;

    public override string ControllerName => "tts-players";

    public async Task<TTSWebPagePlayer> CreateTTSPlayerAsync(TTSWebPagePlayerCreate config)
    {
        ArgumentNullException.ThrowIfNull(config);

        var url = BuildBackendPathVersionRequestBase();

        Console.WriteLine("Creating player:");
        Console.WriteLine($"{url.Url}");

        return await RequestWithRetryAsync(async () =>
        {
            return await url.PostJsonAsync(config)
                .ReceiveJson<TTSWebPagePlayer>()
                .ConfigureAwait(false);
        }).ConfigureAwait(false);
    }

    public async Task DeleteTTSPlayerAsync(Guid id)
    {
        var url = BuildBackendPathVersionRequestBase()
            .AppendPathSegment(id);

        Console.WriteLine("Deleting player:");
        Console.WriteLine($"{url.Url}");

        await RequestWithRetryAsync(async () =>
        {
            return await url.DeleteAsync()
                .ConfigureAwait(false);
        }).ConfigureAwait(false);
    }

    public async Task<TTSWebPagePlayer> GetTTSPlayerAsync(Guid id)
    {
        var url = BuildBackendPathVersionRequestBase()
            .AppendPathSegment(id);

        Console.WriteLine("Querying player:");
        Console.WriteLine($"{url.Url}");

        return await RequestWithRetryAsync(async () =>
        {
            return await url.GetAsync()
                .ReceiveJson<TTSWebPagePlayer>()
                .ConfigureAwait(false);
        }).ConfigureAwait(false);
    }

    public async Task<PaginatedTTSWebPagePlayers> GetTTSPlayersAsync()
    {
        var url = BuildBackendPathVersionRequestBase();

        Console.WriteLine("Querying players:");
        Console.WriteLine($"{url.Url}");

        return await RequestWithRetryAsync(async () =>
        {
            return await url.GetAsync()
                .ReceiveJson<PaginatedTTSWebPagePlayers>()
                .ConfigureAwait(false);
        }).ConfigureAwait(false);
    }

    public async Task<SynthesisMetadataResponse> SynthesisMetadataAsync(
        Guid playerId,
        string sourceLocation,
        string voice,
        IEnumerable<string> xpaths,
        string style = null)
    {
        if (string.IsNullOrWhiteSpace(sourceLocation))
        {
            throw new ArgumentNullException(nameof(sourceLocation));
        }

        if (string.IsNullOrWhiteSpace(voice))
        {
            throw new ArgumentNullException(nameof(voice));
        }

        if (!(xpaths?.Any() ?? false))
        {
            throw new ArgumentNullException(nameof(xpaths));
        }

        var xPathParaValue = JsonConvert.SerializeObject(
            xpaths,
            Formatting.None,
            CommonPublicConst.Json.WriterSettings);

        var url = BuildBackendPathVersionRequestBase()
            .AppendPathSegment("synthesis-metadata")
            .SetQueryParam("playerId", playerId.ToString())
            .SetQueryParam("sourceLocation", sourceLocation)
            .SetQueryParam("voice", voice)
            .SetQueryParam("xpaths", xPathParaValue);
        if (!string.IsNullOrWhiteSpace(style))
        {
            url = url.SetQueryParam("style", style);
        }

        Console.WriteLine("Diagnosing synthesis request:");
        Console.WriteLine($"{url.Url}");

        return await RequestWithRetryAsync(async () =>
        {
            return await url.GetJsonAsync<SynthesisMetadataResponse>()
                .ConfigureAwait(false);
        }).ConfigureAwait(false);
    }
}
