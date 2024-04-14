using Microsoft.Build.Framework;

namespace PetShelter.ViewModels
{
    public class PetTypeEditVM : BaseVM
    {
        [Required]
        
        public string Name { get; set; }
    }
}
