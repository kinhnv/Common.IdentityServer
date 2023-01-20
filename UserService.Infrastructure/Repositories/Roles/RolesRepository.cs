using i3rothers.Infrastructure.Repository;
using UserService.Infrastructure.Entities;
using UserService.Infrastructure.Repositories.Roles;

namespace UserService.Infrastructure.Repositories.Users
{
    public class RolesRepository: Repository<Role>, IRolesRepository
    {
        public RolesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
