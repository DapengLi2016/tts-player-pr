//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.VideoTranslation;

using Microsoft.SpeechServices.CommonLib;
using Microsoft.SpeechServices.CommonLib.Public.Interface;
using Microsoft.SpeechServices.CommonLib.Util;

public class TTSPlayerHttpClientConfig : HttpClientConfigBase
{
    public TTSPlayerHttpClientConfig(IRegionConfig regionConfig, string subKey)
        : base(regionConfig, subKey)
    {
    }

    public override string RouteBase => $"api/texttospeech";

    public override string ApiVersion => CommonPublicConst.ApiVersions.ApiVersion30beta1;

    public override bool IsApiVersionInUrlSegment => true;
}
