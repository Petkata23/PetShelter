using PetShelter.Shared.Attributes;
using System.Collections.Generic;

namespace PetShelter.Data.Entities
{
    public class PetType : BaseEntity
    {
        public PetType()
        {
            this.Pets = new List<Pet>();
        }
        
        public string Name { get; set; }

        public virtual List<Pet> Pets { get; set; }
    }
}
