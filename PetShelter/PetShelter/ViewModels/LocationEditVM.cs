using Microsoft.Build.Framework;

namespace PetShelter.ViewModels
{
    public class LocationEditVM : BaseVM
    {
        [Required]
        public string City { get; set; }
        
        [Required]
        public string Address { get; set; }
        
        [Required]
        public string Country { get; set; }
    }
}
