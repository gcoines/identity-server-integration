using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerIntegration
{
    public class IdentityServerConfig
    {
        public IdentityServerConfig()
        {
            IdentityResources = new List<IdentityResource> { new IdentityResources.OpenId(), new IdentityResources.Profile() };
            ApiResources = new List<ApiResource> { new ApiResource("api1", "My API") };
            Clients = new List<Client>{
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                },
                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                },
                // OpenID Connect hybrid flow and client credentials client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                    AllowOfflineAccess = true
                }
            };
            Users = new List<TestUser> {
                new TestUser
                {
                    SubjectId = Guid.Empty.ToString(),
                    Username = "Alice",
                    Password = "!123$456",
                    ProviderName = "Test provider",
                    ProviderSubjectId = Guid.Empty.ToString(),
                    IsActive = true
                }
            };
        }

        public IEnumerable<IdentityResource> IdentityResources { get; private set; }

        public IEnumerable<ApiResource> ApiResources { get; private set; }

        public IEnumerable<Client> Clients { get; private set; }

        public IEnumerable<TestUser> Users { get; private set; }
    }
}
