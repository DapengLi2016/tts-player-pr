﻿//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.TTSPlayerManagementClient;

using CommandLine;

[Verb("createDemo", HelpText = "Create tts player javascript client demo.")]
public class CreateDemoOptions : OptionsBase
{
    [Option('c', "config", Required = true, HelpText = "Specify TTS player config file path.")]
    public string Config { get; set; }

    [Option('t', "targetDir", Required = false, HelpText = "Specify target dir for the generated TTS player demo.")]
    public string TargetDir { get; set; }
}
