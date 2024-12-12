//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.CommonLib.Attributes;

using Microsoft.SpeechServices.CommonLib.Extensions;
using System;
using System.IO;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class DeploymentEnvironmentAttribute : Attribute
{
    public DeploymentEnvironmentAttribute(
        string regionIdentifier)
    {
        this.RegionIdentifier = regionIdentifier;
    }

    public string RegionIdentifier { get; internal set; }

    public string CustomizedApiHostName { get; set; }

    public int ApiPort { get; set; }

    public string ApiHostName
    {
        get
        {
            if (!string.IsNullOrEmpty(this.CustomizedApiHostName))
            {
                return this.CustomizedApiHostName;
            }
            else if (!string.IsNullOrEmpty(this.RegionIdentifier))
            {
                return $"{RegionIdentifier}.customvoice.api.speech.microsoft.com";
            }

            return string.Empty;
        }
    }

    public string ApiAddress
    {
        get
        {
            var address = ApiHostName;
            if (!string.IsNullOrEmpty(address) && this.ApiPort != 0)
            {
                address = $"{address}:{this.ApiPort}";
            }

            return address;
        }
    }

    public string ApimHostName
    {
        get
        {
            var region = this.RegionIdentifier;

            if (region == "local")
            {
                return this.ApiAddress;
            }

            return $"{this.RegionIdentifier}.api.cognitive.microsoft.com";
        }
    }

    public static TDeploymentEnvironment ParseFromRegionIdentifier<TDeploymentEnvironment>(string regionIdentifier)
        where TDeploymentEnvironment : Enum
    {
        if (string.IsNullOrEmpty(regionIdentifier))
        {
            throw new ArgumentNullException(nameof(regionIdentifier));
        }

        foreach (TDeploymentEnvironment environment in Enum.GetValues(typeof(TDeploymentEnvironment)))
        {
            var attribute = environment.GetAttributeOfType<DeploymentEnvironmentAttribute>();
            if (string.Equals(attribute?.RegionIdentifier, regionIdentifier, StringComparison.OrdinalIgnoreCase))
            {
                return environment;
            }
        }

        throw new NotSupportedException($"Not supported region: {regionIdentifier}");
    }

    public Uri GetApimApiBaseUrl()
    {
        Uri url = null;
        if (!string.IsNullOrEmpty(this.ApimHostName))
        {
            url = new Uri($"https://{this.ApimHostName}/");
        }

        return url;
    }

    public Uri GetBackendApiBaseUrl()
    {
        Uri url = null;
        if (!string.IsNullOrEmpty(this.ApiAddress))
        {
            url = new Uri($"https://{this.ApiAddress}/");
        }

        return url;
    }
}
