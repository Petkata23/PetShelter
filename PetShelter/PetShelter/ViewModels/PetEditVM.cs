using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;

namespace PetShelter.ViewModels
{
    public class PetEditVM : BaseVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public bool IsAdopted { get; set; }

        [Required]
        public bool IsEuthanized { get; set; }

        [Required]
        public IEnumerable<SelectListItem> PetTypeId { get; set; }

        public IEnumerable<SelectListItem> PetTypeList { get; set; }

        public PetTypeDetailsVM PetType { get; set; }

        [Required]
        public int BreedId { get; set; }

        public IEnumerable<SelectListItem> BreedList { get; set; }

        public BreedDetailsVM Breed { get; set; }

        public UserDetailsVM Adopter { get; set; }

        public UserDetailsVM Giver { get; set; }

        public ShelterDetailsVM Shelter { get; set; }
    }
}
