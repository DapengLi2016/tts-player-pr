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

    [EnumMember]
    WithJSONPathFromJSONThenVisualTextFromHTML,

    [EnumMember]
    WithXPathsFromHTML,
}
