using PetShelter.Shared;
using System.ComponentModel.DataAnnotations;

namespace PetShelter.ViewModels
{
    public class BreedEditVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public BreedSize Size { get; set; }
    }
}
