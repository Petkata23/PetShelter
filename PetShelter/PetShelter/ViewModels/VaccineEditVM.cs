using Microsoft.Build.Framework;

namespace PetShelter.ViewModels
{
    public class VaccineEditVM : BaseVM
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
}
