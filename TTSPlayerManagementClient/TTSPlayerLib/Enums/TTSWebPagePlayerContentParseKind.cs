//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.Common;

using System;
using System.Runtime.Serialization;

[DataContract]
public enum TTSWebPagePlayerContentParseKind
{
    [Obsolete("Do not use directly - used to discover deserializer issues.")]
    [EnumMember]
    None = 0,

    // Both MSN and MicrosoftTechCommunity parse HTML content from JSON first, and then extract plain text from HTML.
    // First Parse HTML with JSONPath, and then parse visible text for reading from HTML.
    [EnumMember]
    WithJSONPathFromJSONThenVisualTextFromHTML,

    // Used by WorkLab and ESPN.
    // Configure div name whitelist in player, and when synthesis, proivde the div name in request parameter.
    [EnumMember]
    WithXPathsFromHTML,
}
