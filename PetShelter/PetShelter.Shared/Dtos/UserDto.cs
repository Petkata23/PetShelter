using System.Collections.Generic;

namespace PetShelter.Shared.Dtos
{
    public class UserDto : BaseModel
    {
        public UserDto()
        {
            this.AdoptedPets = new List<PetDto>();
            this.GivenPets = new List<PetDto>();
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? RoleId { get; set; }

        public RoleDto Role { get; set; }

        public int? ShelterId { get; set; }

        public ShelterDto Shelter { get; set; }

        public List<PetDto> AdoptedPets { get; set; }

        public List<PetDto> GivenPets { get; set; }
    }
}
