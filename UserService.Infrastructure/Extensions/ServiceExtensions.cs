using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Services;
using UserService.Infrastructure.IdentityServer;
using UserService.Infrastructure.Repositories.Roles;
using UserService.Infrastructure.Repositories.RoleUsers;
using UserService.Infrastructure.Repositories.Users;
using UserService.Infrastructure.Services;

namespace UserService.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddTransient(this IServiceCollection service)
        {
            service.AddAutoMapperProfiles();

            return service;
        }
    }
}
