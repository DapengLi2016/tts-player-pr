//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.TTSPlayerManagementClient;

using CommandLine;

public partial class OptionsBase
{
    [Option('r', "region", Required = true, HelpText = "Specify region")]
    public string Region { get; set; }

    [Option('s', "subscriptionKey", Required = true, HelpText = "Specify speech resource key.")]
    public string SubscriptionKey { get; set; }
}
