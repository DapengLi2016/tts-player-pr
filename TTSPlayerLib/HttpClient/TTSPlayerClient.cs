//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.Speech.TTSPlayer.HttpClient;

using Flurl;
using Flurl.Http;
using Microsoft.SpeechServices.CommonLib.Util;
using Microsoft.SpeechServices.Cris.Http;
using Microsoft.SpeechServices.VideoTranslationLib.Enums;
using System;
using System.Threading.Tasks;

public class TTSPlayerClient : HttpClientBase<DeploymentEnvironment>
{
    public TTSPlayerClient(HttpClientConfigBase<DeploymentEnvironment> config)
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
}
