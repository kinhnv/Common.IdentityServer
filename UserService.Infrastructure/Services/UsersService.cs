using AutoMapper;
using i3rothers.Domain.Models;
using System.Linq.Expressions;
using UserService.Domain.Models.User;
using UserService.Domain.Services;
using UserService.Infrastructure.Entities;
using UserService.Infrastructure.Repositories.Roles;
using UserService.Infrastructure.Repositories.RoleUsers;
using UserService.Infrastructure.Repositories.Users;

namespace UserService.Infrastructure.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IRoleUsersRepository _roleUsersRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly IMapper _mapper;

        public UsersService(
            IUsersRepository usersRepository,
            IRoleUsersRepository roleUsersRepository,
            IRolesRepository rolesRepository,
            IMapper mapper)
        {
            _usersRepository = usersRepository;
            _roleUsersRepository = roleUsersRepository;
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<UserForDetails>> CreateUserAsync(UserForCreating user)
        {
            var entity = _mapper.Map<User>(user);

            await _usersRepository.CreateAsync(entity);

            var roleUsers = new List<UserRole>();
            user.RoleIds.ForEach(roleId =>
            {
                roleUsers.Add(new UserRole
                {
                    RoleId = roleId,
                    UserId = entity.UserId
                });
            });

            await _roleUsersRepository.CreateAsync(roleUsers);

            return await GetUserAsync(entity.UserId);
        }

        public async Task<ServiceResult> DeleteUserAsync(Guid userId)
        {
            var user = await _usersRepository.GetAsync(userId);

            if (user == null)
                return new ServiceNotFoundResult<UserForDetails>(userId);

            await _rolesRepository.DeleteAsync(userId);
            await _roleUsersRepository.RemoveRoleUserByUserIdAsync(userId);

            return new ServiceResult();
        }

        public async Task<ServiceResult<UserForDetails>> EditUserAsync(UserForEditing user)
        {
            var count = await _usersRepository.CountAsync(x => x.UserId == user.UserId && x.IsDeleted == false);
            if (count == 0)
            {
                return new ServiceNotFoundResult<UserForDetails>(user.UserId);
            }

            await _usersRepository.UpdateAsync(_mapper.Map<User>(user));
            await _roleUsersRepository.RemoveRoleUserByUserIdAsync(user.UserId);
            var roleUsers = new List<UserRole>();
            user.RoleIds.ForEach(roleId =>
            {
                roleUsers.Add(new UserRole
                {
                    RoleId = roleId,
                    UserId = user.UserId
                });
            });
            await _roleUsersRepository.CreateAsync(roleUsers);

            return await GetUserAsync(user.UserId);
        }

        public async Task<ServiceResult<UserForDetails>> GetUserAsync(Guid userId)
        {
            var user = await _usersRepository.GetAsync(userId);

            if (user == null)
                return new ServiceNotFoundResult<UserForDetails>(userId);

            var userForDetails = _mapper.Map<UserForDetails>(user);

            var roleUsers = await _roleUsersRepository.GetAsync(x => x.UserId == user.UserId && x.IsDeleted == false);
            var roleIds = roleUsers.Select(x => x.RoleId).ToList();

            userForDetails.RoleIds = roleIds;

            return new ServiceResult<UserForDetails>(userForDetails);
        }

        public async Task<ServiceResult<GetPaginationResult<UserForList>>> GetUsersAsync(GetUserParams getUserParams)
        {
            Expression<Func<User, bool>> predicate = x => x.IsDeleted == getUserParams.IsDeleted;

            var quetsions = (await _usersRepository.GetAsync(predicate, getUserParams.Skip, getUserParams.Take))
                .Select(section => _mapper.Map<UserForList>(section));
            var count = await _usersRepository.CountAsync(predicate);
            var result = new GetPaginationResult<UserForList>
            {
                Data = quetsions.ToList(),
                Page = getUserParams.Page,
                Take = getUserParams.Take,
                TotalRecord = count
            };

            return new ServiceResult<GetPaginationResult<UserForList>>(result);
        }
    }
}
