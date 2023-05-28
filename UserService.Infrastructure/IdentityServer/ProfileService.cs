using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Security.Claims;
using UserService.Infrastructure.Entities;
using UserService.Infrastructure.Extensions;
using UserService.Infrastructure.Repositories.Users;

namespace UserService.Infrastructure.IdentityServer
{
    public class ProfileService : IProfileService
    {
        private readonly IUsersRepository _usersRepository;

        public ProfileService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        //Get user profile date in terms of claims when calling /connect/userinfo
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                if (!string.IsNullOrEmpty(context?.Subject?.Identity?.Name))
                {
                    var userWithRoles = (await _usersRepository.GetUserWithRolesByUserNameAsync(context.Subject.Identity.Name));

                    if (userWithRoles != null)
                    {
                        var claims = userWithRoles.ToClaims();

                        //set issued claims to return
                        context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                    }
                }
                else if (context != null)
                {
                    var userIdAsString = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? String.Empty;
                    var flag = Guid.TryParse(userIdAsString, out Guid userId);

                    if (flag)
                    {
                        var userWithRoles = await _usersRepository.GetUserWithRolesByUserIdAsync(userId);

                        if (userWithRoles != null)
                        {
                            var claims = userWithRoles.ToClaims();

                            context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //log your error
            }
        }

        //check if user account is active.
        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                //get subject from context (set in ResourceOwnerPasswordValidator.ValidateAsync),
                var userIdAsString = context.Subject.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value ?? String.Empty;
                var flag = Guid.TryParse(userIdAsString, out Guid userId);

                if (flag)
                {
                    var user = await _usersRepository.GetAsync(x => x.UserId == userId);

                    if (user != null)
                    {
                        context.IsActive = true;
                    }
                }
            }
            catch (Exception exception)
            {
                //handle error logging
            }
        }
    }
}
