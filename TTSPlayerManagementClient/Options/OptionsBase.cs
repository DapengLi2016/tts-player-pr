//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.TTSPlayerManagementClient;

using CommandLine;
using Microsoft.SpeechServices.CommonLib.Attributes;
using Microsoft.SpeechServices.VideoTranslationLib.Enums;
using System;

public class OptionsBase
{
    [Option('r', "region", Required = true, HelpText = "Specify region")]
    public string Region { get; set; }

    [Option('v', "subscriptionKey", Required = true, HelpText = "Specify speech resource key.")]
    public string SubscriptionKey { get; set; }

    public DeploymentEnvironment Environment
    {
        get
        {
            if (string.IsNullOrEmpty(this.Region))
            {
                throw new ArgumentNullException(nameof(this.Region));
            }

            return DeploymentEnvironmentAttribute.ParseFromRegionIdentifier<DeploymentEnvironment>(this.Region);
        }
    }
}
