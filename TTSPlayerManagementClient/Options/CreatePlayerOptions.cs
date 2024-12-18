//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.TTSPlayerManagementClient;

using CommandLine;

[Verb("createPlayer", HelpText = "Create tts player.")]
public class CreatePlayerOptions : OptionsBase
{
    [Option('c', "config", Required = true, HelpText = "Specify TTS player config file path.")]
    public string Config { get; set; }
}

