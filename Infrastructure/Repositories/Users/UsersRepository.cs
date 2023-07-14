using i3rothers.Infrastructure.Repository;
using Infrastructure.Entities;

namespace Infrastructure.Repositories.Users
{
    public class UsersRepository: Repository<User>, IUsersRepository
    {
        public UsersRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<UserWithRoles> GetUserWithRolesByUserNameAsync(string userName)
        {
            var usersAsQueryable = _dbContext.Set<User>().Where(x => x.UserName == userName);
            var rolesAsQueryable = _dbContext.Set<Role>().AsQueryable();
            var userRolesAsQueryable = _dbContext.Set<UserRole>().AsQueryable();
            var data = from u in usersAsQueryable
                       join ur in _dbContext.Set<UserRole>() on u.UserId equals ur.UserId
                       join r in rolesAsQueryable on ur.RoleId equals r.RoleId
                       select new
                       {
                           User = u,
                           Role = r
                       };

            var result = data.GroupBy(x => x.User.UserId).Select(x => new UserWithRoles
            {
                User = x.First().User,
                Roles = x.Select(x => x.Role).ToList()
            }).First();

            return await Task.FromResult(result);
        }

        public async Task<UserWithRoles> GetUserWithRolesByUserIdAsync(Guid userId)
        {
            var usersAsQueryable = _dbContext.Set<User>().Where(x => x.UserId == userId);
            var rolesAsQueryable = _dbContext.Set<Role>().AsQueryable();
            var userRolesAsQueryable = _dbContext.Set<UserRole>().AsQueryable();
            var data = from u in usersAsQueryable
                       join ur in _dbContext.Set<UserRole>() on u.UserId equals ur.UserId
                       join r in rolesAsQueryable on ur.RoleId equals r.RoleId
                       select new
                       {
                           User = u,
                           Role = r
                       };

            var result = data.GroupBy(x => x.User.UserId).Select(x => new UserWithRoles
            {
                User = x.First().User,
                Roles = x.Select(x => x.Role).ToList()
            }).First();

            return await Task.FromResult(result);
        }
    }
}
