// <copyright file="SynthesisMetadataResponse.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace Microsoft.SpeechServices.Cris.Http;

using System;
using System.Collections.Generic;

public sealed class SynthesisMetadataResponse
{
    public IEnumerable<string> Ssmls { get; set; }

    public Uri ContentSourceUrl { get; set; }
}