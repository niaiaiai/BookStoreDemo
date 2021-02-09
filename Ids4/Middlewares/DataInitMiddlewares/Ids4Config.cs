using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Ids4.Middlewares.DataInitMiddlewares
{
    public static class Ids4Config
    {
        public static IEnumerable<ApiResource> ApisResource =>
            new List<ApiResource>
            {
                new ApiResource("bookstore", "Book Store API", new [] { JwtClaimTypes.Role, ClaimTypes.NameIdentifier, ClaimTypes.Name })
                {
                    Scopes = { "bookstore" }
                }
            };
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("bookstore")
            };
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("userrole",new string[] { JwtClaimTypes.Role })
            };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "js",
                RequireClientSecret = false,
                AllowOfflineAccess = true,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                SlidingRefreshTokenLifetime = 60 * 5,
                AccessTokenLifetime = 60 * 5,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedCorsOrigins = { "http://localhost:8080" },
                AllowedScopes = {
                    "bookstore",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "userrole",
                    IdentityServerConstants.StandardScopes.OfflineAccess
                }
            }
        };
    }
}
