using Microsoft.Extensions.DependencyInjection;

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
