﻿using i3rothers.AspNetCore.Extensions;
using i3rothers.Domain.InfrastructureRegister;
using i3rothers.Infrastructure.Extensions;
using i3rothers.Infrastructure.Mongodb;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Entities;
using Infrastructure.Extensions;
using Infrastructure.IdentityServer;
using Infrastructure.Repositories.Roles;
using Infrastructure.Repositories.RoleUsers;
using Infrastructure.Repositories.Users;

namespace Infrastructure
{
    public class InfrastructureRegister : IInfrastructureRegister
    {
        public void AddInfrastructure(IServiceCollection service, ConfigurationManager configuration, IWebHostEnvironment environment)
        {
            // Add Services and repositories
            service.AddCaching(configuration);

            service.AddDbContext<ApplicationDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(configuration.GetConfiguration("Mssql:ConnectionString") ?? string.Empty);
            });

            service.AddMongoContext(new MongoDbSettings
            {
                ConnectionString = configuration.GetConfiguration("Mongodb:ConnectionString"),
                Database = configuration.GetConfiguration("Mongodb:Database")
            });

            service.AddAutoMapperProfiles();

            // Repositories
            service.AddTransient<IUsersRepository, UsersRepository>();
            service.AddTransient<IRoleUsersRepository, RoleUsersRepository>();
            service.AddTransient<IRolesRepository, RolesRepository>();

            // Services For Identity
            service.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            service.AddTransient<IProfileService, ProfileService>();

            service.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryApiResources(Config.ApiResources)
                .AddProfileService<ProfileService>();
        }

        public void UseInfrastructure(WebApplication builder)
        {
        }
    }
}
