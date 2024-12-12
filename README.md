# Introduction 
TTS player management client tool 

# Work Flow
   1. Create TTS player with below command:
      TTSPlayerManagementClient.exe createPlayer --region eastus --subscriptionKey YourSpeechResourceKey --config xx
      Config file sample: [CreatePlayerConfigSample.json](TTSPlayerManagementClient/TTSPlayerManagementClient/Config/CreatePlayerConfigSample.json)
   2. List all players:
      TTSPlayerManagementClient.exe list --region eastus --subscriptionKey YourSpeechResourceKey
   3. Query player detail by ID:
      TTSPlayerManagementClient.exe get --region eastus --subscriptionKey YourSpeechResourceKey --id xx
   4. Create javascript client demo:
      TTSPlayerManagementClient.exe get createDemo --region eastus --subscriptionKey YourSpeechResourceKey --config xx --targetDir YourLocalDir
      Config file sample: [CreateDemoConfigSample.json](TTSPlayerManagementClient/TTSPlayerManagementClient/Config/CreateDemoConfigSample.json)
      Tool will prompt your demo page can be found here:
         YourLocalDir\client-sample\flat.html
   5. Delete the player if you don't need anymore.
      TTSPlayerManagementClient.exe delete --region eastus --subscriptionKey YourSpeechResourceKey --id xx

# Getting Started
   Install .NET 7.0:
   https://dotnet.microsoft.com/en-us/download/dotnet/7.0

# Build
   Client Management tool solution: [TTSPlayerManagementClient.sln](TTSPlayerManagementClient.sln)

# Command Line Usage
   | Description | Command line arguments |
   | ------------ | -------------- |
   | List all TTS players. | list --region eastus --subscriptionKey YourSpeechResourceKey |
   | Get TTS player by ID. | get --region eastus --subscriptionKey YourSpeechResourceKey --id xx |
   | Delete TTS player by ID. | delete --region eastus --subscriptionKey YourSpeechResourceKey --id xx |
   | Create TTS Player by config file. | createPlayer --region eastus --subscriptionKey YourSpeechResourceKey --config "[CreatePlayerConfigSample.json](TTSPlayerManagementClient/TTSPlayerManagementClient/Config/CreatePlayerConfigSample.json)" |
   | Create TTS Player javascript client demo. | createDemo --region eastus --subscriptionKey YourSpeechResourceKey --config "[CreateDemoConfigSample.json](TTSPlayerManagementClient/TTSPlayerManagementClient/Config/CreateDemoConfigSample.json)" --targetDir xx |

# Supported regions
   https://learn.microsoft.com/en-us/azure/ai-services/speech-service/regions
   For example: eastus, eastus2 etc.

# Create player configuration

## Sample
```json
{
  "DisplayName": "TTSPlayer",
  "Description": "Player for https://www.your-domain.com/path1/path2/",
  "TtsOutputFormat": "AUDIO-24KHZ-96KBITRATE-MONO-MP3",
  "Properties": {
    "PredefinedUrlPrefix": "https://www.your-domain.com/path1/path2/",
    "AllowedHtmlXPathList": [
      "//div[contains(@class, 'MSTTSPlayerContentSource')]"
    ],
    "AllowedVoiceNameList": [
      "en-US-GuyNeural"
    ],
    "AllowedVoiceStyleList": [
      "general",
      "newscast"
    ]
  }
}
```

## Description
| Property name | Command line arguments |
| ------------ | -------------- |
| DisplayName | TTS player name |
| Description | TTS player description |
| TtsOutputFormat | TTS output format |
| PredefinedUrlPrefix | Your website root path |
| AllowedHtmlXPathList | Predefined html XPath allowed list, maximum 10 supported, only the XPath in this list can be used in synthesis API requset |
| AllowedVoiceNameList | Predefined voice name allowed list, maximum 100 supported, only the voice name in this list can be used in synthesis API requset, please go to [Voice Gallery] in [speech portal](https://speech.microsoft.com/) to get voice name. |
| AllowedVoiceStyleList | Predefined voice style allowed list, maximum 100 supported, only the style in this list can be used in synthesis API requset, please go to [Voice Gallery] in [speech portal](https://speech.microsoft.com/) to get supported styles of the voice. |

## Supported TtsOutputFormat
For now, only support: audio-24khz-96kbitrate-mono-mp3
In Feb 2025, more formats will be supproted when create TTS player:
* audio-16khz-32kbitrate-mono-mp3
* audio-16khz-64kbitrate-mono-mp3
* audio-16khz-128kbitrate-mono-mp3
* audio-24khz-48kbitrate-mono-mp3
* audio-24khz-96kbitrate-mono-mp3
* audio-24khz-160kbitrate-mono-mp3
* audio-48khz-96kbitrate-mono-mp3
* audio-48khz-192kbitrate-mono-mp3


# Create javascript client demo configuration

## Sample
```json
{
  "PlayerId": "BD6C1EA6-1D4C-4AA5-93F3-B0C50425A3CD",
  "ContentSourceLocation": "paht3/path4",
  "VoiceName": "en-US-GuyNeural",
  "VoiceStyle": "general",
  "HtmlXPathList": [
    "//div[contains(@class, 'MSTTSPlayerContentSource')]"
  ]
}

```

## Description
| Property name | Command line arguments |
| ------------ | -------------- |
| PlayerId | TTS player ID used for synthesis request. |
| ContentSourceLocation | The webpage location used for synthesis request, for example if the value is paht3/path4, and the PredefinedUrlPrefix of player is https://www.your-domain.com/path1/path2/, then the player will synthesis web page https://www.your-domain.com/path1/path2/paht3/path4 |
| VoiceName | TTS voice name, the voice name need to be included in the AllowedVoiceNameList of the player |
| VoiceStyle | TTS voice style name, the style name need to be included in the AllowedVoiceStyleList of the player |
| HtmlXPathList | XPath list used for extracting plain text for TTS synthesizing, the xPath need to be included in the AllowedHtmlXPathList of the player |

