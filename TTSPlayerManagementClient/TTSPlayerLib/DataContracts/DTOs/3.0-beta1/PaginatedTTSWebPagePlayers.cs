﻿//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.Cris.Http;

using System;
using System.Collections.Generic;

public sealed class PaginatedTTSWebPagePlayers
{
    public List<TTSWebPagePlayer> Values { get; private set; }

    public Uri NextLink { get; private set; }
}
