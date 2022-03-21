// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServerHost.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users
        {
            get
            {

                return new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "818727",
                        Username = "mirza",
                        Password = "@Future30",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Mirza Ghulam Rasyid"),
                            new Claim(JwtClaimTypes.Email, "ghulamcyber@hotmail.com")
                        }
                    }
                };
            }
        }
    }
}