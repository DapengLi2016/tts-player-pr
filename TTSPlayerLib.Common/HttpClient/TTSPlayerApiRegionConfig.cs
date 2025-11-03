//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.CommonLib.Public.Interface;

using System;

public class TTSPlayerApiRegionConfig : ITTSPlayerRegionConfig
{
    public TTSPlayerApiRegionConfig(string regionIdentifier)
    {
        ArgumentNullException.ThrowIfNull(regionIdentifier);
        this.RegionIdentifier = regionIdentifier;
    }

    public string RegionIdentifier { get; private set; }

    // TODO: split API host and synthesis host
    public virtual string SynthesisApiHostName => $"{this.RegionIdentifier}.customvoice.api.speech.microsoft.com";

    public virtual string ManagementApiHostName => $"{this.RegionIdentifier}.api.cognitive.microsoft.com";

    public virtual Uri EndpointUrl => new Uri($"https://{ManagementApiHostName}");
}
