using i3rothers.Infrastructure.Repository;
using Infrastructure.Entities;

namespace Infrastructure.Repositories.RoleUsers
{
    public class RoleUsersRepository: Repository<UserRole>, IRoleUsersRepository
    {
        public RoleUsersRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
