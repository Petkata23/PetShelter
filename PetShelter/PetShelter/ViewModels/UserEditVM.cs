using Microsoft.Build.Framework;

namespace PetShelter.ViewModels
{
    public class UserEditVM : BaseVM
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public RoleDetailsVM Role { get; set; }
        
        [Required]
        public ShelterDetailsVM Shelter { get; set; }
    }
}
