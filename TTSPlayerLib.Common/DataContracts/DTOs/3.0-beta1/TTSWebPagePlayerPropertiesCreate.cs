//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.Cris.Http;

using Microsoft.SpeechServices.Common;
using System;
using System.Collections.Generic;

public class TTSWebPagePlayerPropertiesCreate
{
    public TTSWebPagePlayerPropertiesCreate()
    {
        this.AllowedHtmlXPathList = new List<string>();
        this.AllowedVoiceNameList = new List<string>();
        this.AllowedVoiceStyleList = new List<string>();
    }

    public TTSWebPagePlayerContentParseKind ParseKind { get; set; }

    public Uri PredefinedUrlPrefix { get; set; }

    public List<string> AllowedHtmlXPathList { get; private set; }

    public List<string> AllowedVoiceNameList { get; private set; }

    public List<string> AllowedVoiceStyleList { get; private set; }
}