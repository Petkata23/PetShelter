using System.ComponentModel.DataAnnotations;

namespace PetShelter.ViewModels
{
    public class PetVaccineTypeEditVM : BaseVM
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
