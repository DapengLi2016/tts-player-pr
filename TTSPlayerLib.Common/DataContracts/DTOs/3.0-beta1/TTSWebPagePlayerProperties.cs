﻿//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.Cris.Http;

using System;
using System.Collections.Generic;
using Microsoft.SpeechServices.Common;

public class TTSWebPagePlayerProperties
{
    public string TtsOutputFormat { get; set; }

    public TTSWebPagePlayerContentParseKind ParseKind { get; set; }

    public Uri PredefinedUrlPrefix { get; set; }

    public IEnumerable<string> JsonPathList { get; set; }

    public IEnumerable<string> AllowedHtmlXPathList { get; set; }

    public IEnumerable<string> AllowedVoiceNameList { get; set; }

    public IEnumerable<string> AllowedVoiceStyleList { get; set; }
}
