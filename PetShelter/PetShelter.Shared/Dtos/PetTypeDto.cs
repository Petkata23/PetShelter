using System.Collections.Generic;

namespace PetShelter.Shared.Dtos
{
    public class PetTypeDto : BaseModel
    {
        public PetTypeDto()
        {
            this.Pets = new List<PetDto>();
        }

        public string Name { get; set; }

        public List<PetDto> Pets { get; set; }
    }
}
