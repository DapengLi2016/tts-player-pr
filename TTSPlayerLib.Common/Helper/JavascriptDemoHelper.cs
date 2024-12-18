//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.TTSPlayerLib;

using Microsoft.SpeechServices.CommonLib.TtsUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

public static class JavascriptDemoHelper
{
    public async static Task GenerateJavascriptClientDemoHelperAsync(
        Guid playerId,
        string hostName,
        string sourceLocation,
        string voice,
        string style,
        IList<string> xPaths,
        string targetDir)
    {
        if (string.IsNullOrEmpty(hostName))
        {
            throw new ArgumentNullException(nameof(hostName));
        }

        const string MainPagePath = @"client-sample\flat.html";
        if (string.IsNullOrWhiteSpace(sourceLocation))
        {
            throw new ArgumentNullException(nameof(sourceLocation));
        }

        if ((xPaths?.Count ?? 0) == 0)
        {
            throw new ArgumentException($"{nameof(xPaths)} should not be empty.");
        }

        var sourceDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "JavascriptClient");
        CommonHelper.ThrowIfDirectoryNotExist(sourceDir);

        CommonHelper.CopyDirectory(sourceDir, targetDir, true);

        var filePath = Path.Combine(sourceDir, MainPagePath);
        CommonHelper.ThrowIfFileNotExist(filePath);
        var content = await File.ReadAllTextAsync(filePath).ConfigureAwait(false);
        content = content.Replace($"[{TTSPlayerConstant.ClientDemoPlaceholders.PlayerId}]", playerId.ToString());
        content = content.Replace($"[{TTSPlayerConstant.ClientDemoPlaceholders.APIEndpoint}]", hostName);
        content = content.Replace($"[{TTSPlayerConstant.ClientDemoPlaceholders.SourceLocation}]", sourceLocation);
        content = content.Replace($"[{TTSPlayerConstant.ClientDemoPlaceholders.Voice}]", voice);
        if (string.IsNullOrWhiteSpace(style))
        {
            style = "general";
        }

        content = content.Replace($"[{TTSPlayerConstant.ClientDemoPlaceholders.Style}]", style);
        var xPath = string.Join(',', xPaths.Select(x => $"\"{x}\""));
        content = content.Replace($"[{TTSPlayerConstant.ClientDemoPlaceholders.XPaths}]", $"[{xPath}]");

        var targetFilePath = Path.Combine(targetDir, MainPagePath);
        CommonHelper.EnsureFolderExist(targetDir);
        await File.WriteAllTextAsync(targetFilePath, content).ConfigureAwait(false);
    }
}
