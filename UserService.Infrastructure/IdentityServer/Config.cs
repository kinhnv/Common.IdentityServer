using IdentityModel;
using IdentityServer4.Models;

namespace UserService.Infrastructure.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
        {
            new ApiResource
            {
                Name = "test-service",
                ApiSecrets = { new Secret("secret".Sha256()) },
                UserClaims = {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.PhoneNumber,
                    JwtClaimTypes.PhoneNumberVerified,
                    JwtClaimTypes.GivenName,
                    JwtClaimTypes.FamilyName,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Role
                },
                Enabled = true,
                Scopes = {
                    "test-maker"
                }
            },
            new ApiResource
            {
                Name = "event-service",
                ApiSecrets = { new Secret("secret".Sha256()) },
                UserClaims = {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.PhoneNumber,
                    JwtClaimTypes.PhoneNumberVerified,
                    JwtClaimTypes.GivenName,
                    JwtClaimTypes.FamilyName,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Role
                },
                Enabled = true,
                Scopes = {
                    "test-maker"
                }
            },
            new ApiResource
            {
                Name = "user-service",
                ApiSecrets = { new Secret("secret".Sha256()) },
                UserClaims = {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.PhoneNumber,
                    JwtClaimTypes.PhoneNumberVerified,
                    JwtClaimTypes.GivenName,
                    JwtClaimTypes.FamilyName,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Role
                },
                Enabled = true,
                Scopes = {
                    "test-maker"
                }
            }
        };

        
        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
        {
            new ApiScope("test-maker", "TestMaker"),
        };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "test-player",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    new Secret("test-player".Sha256())
                },
                AllowedScopes = { "test-maker" },
                AllowOfflineAccess = true,
                RefreshTokenUsage = TokenUsage.OneTimeOnly
            },
            new Client
            {
                ClientId = "administrator",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    new Secret("administrator".Sha256())
                },
                AllowedScopes = { "test-maker" },
                AllowOfflineAccess = true,
                RefreshTokenUsage = TokenUsage.OneTimeOnly
            }
        };
    }
}
