﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Casgem_Microservis.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
                new ApiResource("resource_photo_stock"){Scopes={"photo_stock_fullpermission"}},
                    new ApiResource("resource_basket"){Scopes={"basket_fullpermission"}},
                        new ApiResource("resource_discount"){Scopes={"discount_fullpermission"}},
                            new ApiResource("resource_order"){Scopes={"order_fullpermission"}},
                                new ApiResource("resource_payment"){Scopes={"payment_fullpermission"}},
                                    new ApiResource("resource_gateway"){Scopes={"gateway_fullpermission"}},
                                        new ApiResource("resource_cargo"){Scopes={"cargo_fullpermission"}},

                                        new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };                 
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource(){Name="roles",DisplayName="Roles",Description="Kullanıcı Rolleri",UserClaims=new []{"role"}}
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_fullpermission","Katalog API full erişim"),
                    new ApiScope("photo_stock_fullpermission","Photo Stock API için full erişim"),
                        new ApiScope("basket_fullpermission","Sepet API için full erişim"),
                            new ApiScope("discount_fullpermission","Discount API için full erişim"),
                                new ApiScope("order_fullpermission","Sipariş API için full erişim"),
                                    new ApiScope("payment_fullpermission","Payment API için full erişim"),
                                        new ApiScope("gateway_fullpermission","Gateway API için full erişim"),
                                            new ApiScope("cargo_fullpermission","Kargo API için full erişim"),
                                            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "CoreClient1",
                    ClientName = "Casgem1",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes={"catalog_fullpermission","photo_stock_fulpermission",
                        IdentityServerConstants.LocalApi.ScopeName}
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "CoreClient2",
                    ClientName = "Casgem2",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes={"basket_fullpermission","order_fulpermission","discount_fullpermission","cargo_fullpermission",
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.LocalApi.ScopeName,"roles"
                },
            };
    }
}