using System.Collections.Generic;

namespace PetShelter.Shared.Dtos
{
    public class VaccineDto : BaseModel
    {
        public VaccineDto()
        {
            this.PetVaccines = new List<PetVaccineDto>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<PetVaccineDto> PetVaccines { get; set; }
    }
}
