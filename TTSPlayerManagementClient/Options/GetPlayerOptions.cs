//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.TTSPlayerManagementClient;

using CommandLine;
using System;

[Verb("get", HelpText = "Get tts player.")]
public class GetPlayerOptions : OptionsBase
{
    [Option('i', "id", Required = true, HelpText = "Specify player ID.")]
    public Guid Id { get; set; }
}

