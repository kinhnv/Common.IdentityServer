using AutoMapper;
using i3rothers.Domain.Models;
using System.Linq.Expressions;
using UserService.Domain.Models;
using UserService.Domain.Models.Role;
using UserService.Domain.Services;
using UserService.Infrastructure.Entities;
using UserService.Infrastructure.Repositories.Roles;

namespace UserService.Infrastructure.Services
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _rolesRepository;
        private readonly IMapper _mapper;

        public RolesService(IRolesRepository rolesRepository, IMapper mapper)
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<RoleForDetails>> CreateRoleAsync(RoleForCreating role)
        {
            var entity = _mapper.Map<Role>(role);

            await _rolesRepository.CreateAsync(entity);

            return await GetRoleAsync(entity.RoleId);
        }

        public async Task<ServiceResult> DeleteRoleAsync(Guid roleId)
        {
            var role = await _rolesRepository.GetAsync(roleId);
            if (role == null || role.IsDeleted == true)
            {
                return new ServiceNotFoundResult<Role>(roleId);
            }
            role.IsDeleted = true;
            await EditRoleAsync(_mapper.Map<RoleForEditing>(role));
            return new ServiceResult();
        }

        public async Task<ServiceResult<RoleForDetails>> EditRoleAsync(RoleForEditing role)
        {
            var entity = _mapper.Map<Role>(role);

            var result = await _rolesRepository.GetAsync(role.RoleId);
            if (result == null || result.IsDeleted == true)
            {
                return new ServiceNotFoundResult<RoleForDetails>(role.RoleId.ToString());
            }

            await _rolesRepository.UpdateAsync(entity);
            return await GetRoleAsync(entity.RoleId);
        }

        public async Task<ServiceResult<RoleForDetails>> GetRoleAsync(Guid roleId)
        {
            var role = await _rolesRepository.GetAsync(roleId);

            if (role == null)
                return new ServiceNotFoundResult<RoleForDetails>(roleId);

            return await Task.FromResult(new ServiceResult<RoleForDetails>(_mapper.Map<RoleForDetails>(role)));
        }

        public async Task<ServiceResult<GetPaginationResult<RoleForList>>> GetRolesAsync(GetRolesParams getRolesParams)
        {
            Expression<Func<Role, bool>> predicate = x => x.IsDeleted == getRolesParams.IsDeleted;

            var quetsions = (await _rolesRepository.GetAsync(predicate, getRolesParams.Skip, getRolesParams.Take))
                .Select(section => _mapper.Map<RoleForList>(section));
            var count = await _rolesRepository.CountAsync(predicate);
            var result = new GetPaginationResult<RoleForList>
            {
                Data = quetsions.ToList(),
                Page = getRolesParams.Page,
                Take = getRolesParams.Take,
                TotalRecord = count
            };

            return new ServiceResult<GetPaginationResult<RoleForList>>(result);
        }

        public async Task<ServiceResult<IEnumerable<SelectOption>>> GetRolesAsSelectOptionsAsync()
        {
            return new ServiceResult<IEnumerable<SelectOption>>((await _rolesRepository.GetAsync()).Select(x => new SelectOption
            {
                Title = x.Name,
                Value = x.RoleId.ToString()
            }));
        }
    }
}
