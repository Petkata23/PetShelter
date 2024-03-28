using System.Collections.Generic;

namespace PetShelter.Shared.Dtos
{
    public class RoleDto : BaseModel
    {
        public RoleDto()
        {
            this.Users = new List<UserDto>();
        }

        public string Name { get; set; }

        public List<UserDto> Users { get; set; }
    }
}
