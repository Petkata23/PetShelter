using AutoMapper;
using PetShelter.Data.Entities;
using PetShelter.Shared.Attributes;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PetShelter.Data.Repos
{
    [AutoBind]
    public class RoleRepository : BaseRepository<Role, RoleDto>, IRoleRepository
    {
        public RoleRepository(PetShelterDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<RoleDto> GetByNameIfExistsAsync(string roleName)
        {
            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));

            return mapper.Map<RoleDto>(role);
        }
    }
}

