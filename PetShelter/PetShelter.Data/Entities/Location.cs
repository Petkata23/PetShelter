using PetShelter.Shared.Attributes;

namespace PetShelter.Data.Entities
{
    public class Location : BaseEntity
    {        
        public string City { get; set; }
                
        public string Address { get; set; }
                
        public string Country { get; set; }

        public int? ShelterId { get; set; }

        public virtual Shelter Shelter { get; set; }
    }
}