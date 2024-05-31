using System;
using System.Threading.Tasks;
using PetShelter.Shared.Attributes;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;

namespace PetShelter.Services.Services
{
    [AutoBind]
    public class RolesService : BaseCrudService<RoleDto, IRoleRepository>, IRolesService
    {
        private readonly IRoleRepository roleRepository;

        public RolesService(IRoleRepository roleRepository) : base(roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<IEnumerable<RoleDto>> GetAllActiveAsync()
        {
            var allRoles = await roleRepository.GetAllAsync();
            return allRoles;
        }

        public Task<RoleDto> GetByNameIfExistsAsync(string roleName)
        {
            return roleRepository.GetByNameIfExistsAsync(roleName);
        }
    }
}

