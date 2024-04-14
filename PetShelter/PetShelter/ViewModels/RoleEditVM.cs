using Microsoft.Build.Framework;

namespace PetShelter.ViewModels
{
    public class RoleEditVM : BaseVM
    {
        [Required]
        public string Name { get; set; }
    }
}
