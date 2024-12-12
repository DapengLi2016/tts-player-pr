//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//

namespace Microsoft.SpeechServices.CommonLib.Util;

using Flurl;
using Flurl.Http;
using Microsoft.SpeechServices.CommonLib.Attributes;
using Microsoft.SpeechServices.CommonLib.Extensions;
using System;

public abstract class HttpClientConfigBase<TDeploymentEnvironment>
    where TDeploymentEnvironment : Enum
{
    public HttpClientConfigBase(TDeploymentEnvironment environment, string subKey)
    {
        this.Environment = environment;
        this.SubscriptionKey = subKey;
    }

    public virtual Uri RootUrl
    {
        get
        {
            // Use APIM for public API.
            var url = this.BaseUrl
                .AppendPathSegment(RouteBase);

            if (this.IsApiVersionInUrlSegment)
            {
                url = url.AppendPathSegment(this.ApiVersion);
            }
            else
            {
                url = url.SetQueryParam("api-version", this.ApiVersion);
            }

            return url.ToUri();
        }
    }

    public virtual bool IsApiVersionInUrlSegment => false;

    public virtual string ApiVersion { get; set; }

    public abstract string RouteBase { get; }

    public TDeploymentEnvironment Environment { get; set; }

    public string SubscriptionKey { get; set; }

    public virtual Uri BaseUrl
    {
        get
        {
            return this.Environment.GetAttributeOfType<DeploymentEnvironmentAttribute>()?.GetBackendApiBaseUrl();
        }
    }
}
