using System.Collections.Generic;

namespace PetShelter.Shared.Dtos
{
    public class PetDto : BaseModel
    {
        public PetDto()
        {
            this.PetVaccines = new List<PetVaccineDto>();
        }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Color { get; set; }

        public bool IsAdopted { get; set; }

        public bool IsEuthanized { get; set; }

        public int PetTypeId { get; set; }

        public PetTypeDto PetType { get; set; }

        public int BreedId { get; set; }

        public BreedDto Breed { get; set; }

        public int? AdopterId { get; set; }

        public UserDto Adopter { get; set; }

        public int? GiverId { get; set; }

        public UserDto Giver { get; set; }

        public int? ShelterId { get; set; }

        public ShelterDto Shelter { get; set; }

        public List<PetVaccineDto> PetVaccines { get; set; }
    }
}
