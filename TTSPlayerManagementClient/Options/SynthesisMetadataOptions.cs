//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.TTSPlayerManagementClient;

using CommandLine;

[Verb("synthesisMetadata", HelpText = "Request synthesis metadata for diagnosing purpose.")]
public class SynthesisMetadataOptions : OptionsBase
{
    [Option('c', "config", Required = true, HelpText = "Specify TTS player demo creation config.")]
    public string Config { get; set; }
}

