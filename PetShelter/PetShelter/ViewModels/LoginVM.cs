using Microsoft.Build.Framework;

namespace PetShelter.ViewModels
{
    public class LoginVM : BaseVM
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
