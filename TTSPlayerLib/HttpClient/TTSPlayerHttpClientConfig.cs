//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.VideoTranslation;

using Microsoft.SpeechServices.CommonLib;
using Microsoft.SpeechServices.CommonLib.Attributes;
using Microsoft.SpeechServices.CommonLib.Extensions;
using Microsoft.SpeechServices.CommonLib.Util;
using Microsoft.SpeechServices.VideoTranslationLib.Enums;
using System;

public class TTSPlayerHttpClientConfig : HttpClientConfigBase<DeploymentEnvironment>
{
    public TTSPlayerHttpClientConfig(DeploymentEnvironment environment, string subKey)
        : base(environment, subKey)
    {
    }

    public override string RouteBase => $"api/texttospeech";
        // throw new NotImplementedException();

    public override string ApiVersion => CommonConst.ApiVersions.ApiVersion30beta1;

    public override bool IsApiVersionInUrlSegment => true;

    public override Uri BaseUrl
    {
        get
        {
            var hostWithPort = this.Environment.GetAttributeOfType<DeploymentEnvironmentAttribute>()?.ApiAddress;
            if (string.IsNullOrWhiteSpace(hostWithPort))
            {
                return null;
            }

            return new Uri($"https://{hostWithPort}");
        }
    }
}
