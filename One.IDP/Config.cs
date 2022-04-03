// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace One.IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("One.Read"),
                new ApiScope("One.Write"),
                new ApiScope("Day1Lab.FullAccess")
            };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("One.System","One System Apis")
            {
                Scopes =
                {
                    "One.Read",
                    "One.Write"
                },
                UserClaims =
                {
                    "name",
                    "email"
                }
            },
             new ApiResource("Day1Lab.Template","Day1Lab Template Apis")
            {
                Scopes =
                {
                    "Day1Lab.FullAccess"
                },
                UserClaims =
                {
                    "name",
                    "email"
                }
            }
        };


        public static IEnumerable<Client> Clients(IConfiguration configuration) =>
            new Client[]
            {
                new Client
                {
                    ClientId = "One.System",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = {
                        $"{configuration["Endpoints:OneReadApi"]}/OneApi/Read/swagger/oauth2-redirect.html",
                        $"{configuration["Endpoints:OneWriteApi"]}/OneApi/Write/swagger/oauth2-redirect.html",
                        $"{configuration["Endpoints:OneGateway"]}/swagger/oauth2-redirect.html"

                    },
                     AllowedCorsOrigins =
                    {
                        configuration["Endpoints:OneReadApi"],
                        configuration["Endpoints:OneWriteApi"],
                        configuration["Endpoints:OneGateway"]
                    },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "email","One.Read","One.Write","offline_access" }
                },
                new Client
                {
                    ClientId = "Day1Lab.Template",
                    ClientSecrets = { new Secret("5cea305b-406e-4b75-a7c9-260d6fac6d6a".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = {
                        $"{configuration["Endpoints:Day1LabCommandApi"]}/swagger/oauth2-redirect.html",
                        $"{configuration["Endpoints:Day1LabQueryApi"]}/swagger/oauth2-redirect.html",
                        $"{configuration["Endpoints:Day1LabGateway"]}/swagger/oauth2-redirect.html"

                    },
                     AllowedCorsOrigins =
                    {
                        configuration["Endpoints:Day1LabCommandApi"],
                        configuration["Endpoints:Day1LabQueryApi"],
                        configuration["Endpoints:Day1LabGateway"]
                    },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "email", "Day1Lab.FullAccess", "offline_access" }
                },
            };
    }
}