//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.Cris.Http;

using Microsoft.SpeechServices.Cris.Http.DTOs.Public;

public sealed class TTSWebPagePlayer : StatefulResourceBase
{
    public TTSWebPagePlayerProperties Properties { get; set; }
}