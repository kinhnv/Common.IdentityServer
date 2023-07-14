using i3rothers.Infrastructure.Repository;
using Infrastructure.Entities;
using Infrastructure.Repositories.Roles;

namespace Infrastructure.Repositories.Users
{
    public class RolesRepository: Repository<Role>, IRolesRepository
    {
        public RolesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
