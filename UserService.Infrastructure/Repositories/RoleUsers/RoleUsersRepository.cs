using i3rothers.Infrastructure.Repository;
using UserService.Infrastructure.Entities;

namespace UserService.Infrastructure.Repositories.RoleUsers
{
    public class RoleUsersRepository: Repository<UserRole>, IRoleUsersRepository
    {
        public RoleUsersRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task RemoveRoleUserByUserIdAsync(Guid userId)
        {
            var roleUsers = _dbContext.Set<UserRole>().Where(x => x.UserId == userId).ToList();
            _dbContext.Set<UserRole>().RemoveRange(roleUsers);
            await _dbContext.SaveChangesAsync();
        }
    }
}
