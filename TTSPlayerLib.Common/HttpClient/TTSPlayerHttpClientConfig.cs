//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.VideoTranslation;

using Microsoft.SpeechServices.CommonLib;
using Microsoft.SpeechServices.CommonLib.Public.Interface;
using Microsoft.SpeechServices.CommonLib.Util;
using System;

public class TTSPlayerHttpClientConfig : HttpSpeechClientConfigBase
{
    public TTSPlayerHttpClientConfig(
        ITTSPlayerRegionConfig regionConfig,
        string subKey,
        string customDomainName,
        Guid? managedIdentityClientId)
        : base(
            regionConfig: regionConfig,
            subKey: subKey,
            customDomainName: customDomainName,
            managedIdentityClientId: managedIdentityClientId)
    {
    }

    public ITTSPlayerRegionConfig TTSPlayerRegionConfig => this.RegionConfig as ITTSPlayerRegionConfig;

    // public override string RouteBase => string.IsNullOrWhiteSpace(this.CustomDomainName)?
    //    "api/texttospeech" : "texttospeech/ttsplayer";
    public override string RouteBase => "texttospeech/ttsplayer";
    
    public override string ApiVersion => CommonPublicConst.ApiVersions.ApiVersion30beta1;

    public override bool IsApiVersionInUrlSegment => true;
}
