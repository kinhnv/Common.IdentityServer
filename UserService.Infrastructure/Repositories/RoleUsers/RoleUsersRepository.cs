using i3rothers.Infrastructure.Repository;
using UserService.Infrastructure.Entities;

namespace UserService.Infrastructure.Repositories.RoleUsers
{
    public class RoleUsersRepository: Repository<UserRole>, IRoleUsersRepository
    {
        public RoleUsersRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
