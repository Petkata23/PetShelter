using System.Collections.Generic;

namespace PetShelter.Shared.Dtos
{
    public class ShelterDto : BaseModel
    {
        public ShelterDto()
        {
            this.Pets = new List<PetDto>();
            this.Employees = new List<UserDto>();
        }

        public int PetCapacity { get; set; }

        public int LocationId { get; set; }

        public LocationDto Location { get; set; }

        public List<UserDto> Employees { get; set; }

        public List<PetDto> Pets { get; set; }
    }
}
