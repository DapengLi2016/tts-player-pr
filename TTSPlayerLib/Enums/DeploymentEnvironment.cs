//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.VideoTranslationLib.Enums;

using Microsoft.SpeechServices.CommonLib.Attributes;
using System.Runtime.Serialization;

[DataContract]
public enum DeploymentEnvironment
{
    [EnumMember]
    Default,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "local",
        CustomizedApiHostName = "localhost",
        ApiPort = 44311)]
    Local,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "developeus",
        CustomizedApiHostName = "developeus.customvoice.api.speech-test.microsoft.com")]
    DevelopEUS,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "australiaeast")]
    ProductionAUE,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "brazilsouth")]
    ProductionBRS,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "canadacentral")]
    ProductionCAC,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "westus")]
    ProductionUSW,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "westus3")]
    ProductionUSW3,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "westcentralus")]
    ProductionUSWC,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "eastasia")]
    ProductionEA,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "eastus")]
    ProductionEUS,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "eastus2")]
    ProductionEUS2,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "francecentral")]
    ProductionFC,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "germanywestcentral")]
    ProductionGWC,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "centralindia")]
    ProductionINC,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "jioindiawest")]
    ProductionJINW,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "japaneast")]
    ProductionJPE,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "japanwest")]
    ProductionJPW,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "koreacentral")]
    ProductionKC,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "northeurope")]
    ProductionNEU,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "norwayeast")]
    ProductionNOE,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "qatarcentral")]
    ProductionQAC,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "southafricanorth")]
    ProductionSAN,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "southeastasia")]
    ProductionSEA,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "swedencentral")]
    ProductionSEC,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "switzerlandnorth")]
    ProductionSWN,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "switzerlandwest")]
    ProductionSWW,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "uaenorth")]
    ProductionUAEN,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "uksouth")]
    ProductionUKS,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "centralus")]
    ProductionUSC,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "northcentralus")]
    ProductionUSNC,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "southcentralus")]
    ProductionUSSC,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "westeurope")]
    ProductionWEU,

    [EnumMember]
    [DeploymentEnvironment(
        regionIdentifier: "westus2")]
    ProductionWUS2,
}