using i3rothers.Infrastructure.Repository;
using UserService.Infrastructure.Entities;

namespace UserService.Infrastructure.Repositories.RoleUsers
{
    public interface IRoleUsersRepository: IRepository<UserRole>
    {
        Task RemoveRoleUserByUserIdAsync(Guid userId);
    }
}
