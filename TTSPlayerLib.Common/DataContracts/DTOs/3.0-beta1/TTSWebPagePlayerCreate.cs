//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.Cris.Http;

using System;

public sealed class TTSWebPagePlayerCreate
{
    public string DisplayName { get; set; }

    public string Description { get; set; }

    public string TtsOutputFormat { get; set; }

    public TTSWebPagePlayerPropertiesCreate Properties { get; set; }
}