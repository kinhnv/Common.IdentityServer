using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Models.Role;
using UserService.Domain.Models.User;
using UserService.Infrastructure.Entities;

namespace UserService.Infrastructure.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            return service;
        }
    }
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserForList>();
            CreateMap<UserForCreating, User>();
            CreateMap<UserForEditing, User>();
            CreateMap<User, UserForEditing>();
            CreateMap<User, UserForDetails>();

            CreateMap<Role, RoleForList>();
            CreateMap<RoleForCreating, Role>();
            CreateMap<RoleForEditing, Role>();
            CreateMap<Role, RoleForEditing>();
            CreateMap<Role, RoleForDetails>();        }
    }
}
