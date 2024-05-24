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

namespace PetShelter.Data.Repos
{
    [AutoBind]
    public class RoleRepository : BaseRepository<Role, RoleDto>, IRoleRepository
    {
        public RoleRepository(PetShelterDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        private readonly List<RoleDto> roles = new List<RoleDto>
        {
            new RoleDto { Id = 1, Name = "Admin" },
            new RoleDto { Id = 2, Name = "User" }
            // Add more roles as needed
        };

        public async Task<RoleDto> GetByNameIfExistsAsync(string roleName)
        {
            return await Task.FromResult(roles.FirstOrDefault(r => r.Name == roleName));
        }
    }
}

