# Introduction 
TTS player management client tool 

# Getting Started
   Install .NET 7.0:
   https://dotnet.microsoft.com/en-us/download/dotnet/7.0

# Build
   Client Management tool solution: [TTSPlayerManagementClient.sln](TTSPlayerManagementClient.sln)

# Command line usage
   | Description | Command line arguments |
   | ------------ | -------------- |
   | List all TTS players. | list --region eastus --subscriptionKey xx |
   | Get TTS player by ID. | get --region eastus --subscriptionKey xx --id xx |
   | Delete TTS player by ID. | delete --region eastus --subscriptionKey xx --id xx |
   | Create TTS Player by config file. | createPlayer --region eastus --subscriptionKey xx --config "[CreatePlayerConfigSample.json](TTSPlayerManagementClient/Config/CreatePlayerConfigSample.json)" |
   | Create TTS Player javascript client demo. | createDemo --region eastus --subscriptionKey xx --config "[CreateDemoConfigSample.json](TTSPlayerManagementClient/Config/CreateDemoConfigSample.json)" --targetDir xx |

# Supported regions
   https://learn.microsoft.com/en-us/azure/ai-services/speech-service/regions