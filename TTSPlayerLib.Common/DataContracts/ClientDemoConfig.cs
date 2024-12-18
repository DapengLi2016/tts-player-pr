//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.Cris.Http;

using System;
using System.Collections.Generic;

public sealed class ClientDemoConfig
{
    public Guid PlayerId { get; set; }

    public string ContentSourceLocation { get; set; }

    public string VoiceName { get; set; }

    public string VoiceStyle { get; set; }

    public List<string> HtmlXPathList { get; set; }
}
