using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShelter.Services.Contracts;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;

namespace PetShelter.Shared.Services.Contracts
{
    public interface IRolesService : IBaseCrudService<RoleDto, IRoleRepository>
    {
        Task<RoleDto> GetByNameIfExistsAsync(string roleName);
    }
}
