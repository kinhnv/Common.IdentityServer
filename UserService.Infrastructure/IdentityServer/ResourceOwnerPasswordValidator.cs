using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using UserService.Infrastructure.Entities;
using UserService.Infrastructure.Extensions;
using UserService.Infrastructure.Repositories.Users;

namespace UserService.Infrastructure.IdentityServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<IResourceOwnerPasswordValidator> _logger;

        public ResourceOwnerPasswordValidator(IUsersRepository usersRepository, ILogger<IResourceOwnerPasswordValidator> logger)
        {
            _usersRepository = usersRepository;
            _logger = logger;
        }

        //this is used to validate your user account with provided grant at /connect/token
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                //get your user model from db (by username - in my case its email)
                var userWithRoles = (await _usersRepository.GetUserWithRolesByUserNameAsync(context.UserName));
                if (userWithRoles != null)
                {
                    if (userWithRoles.User.Password == context.Password)
                    {
                        context.Result = new GrantValidationResult(
                            subject: userWithRoles.User.UserId.ToString(),
                            authenticationMethod: "custom",
                            claims: userWithRoles.ToClaims()
                        );

                        return;
                    }

                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                    return;
                }
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
        }
    }
}
