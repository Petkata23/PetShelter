using Microsoft.Build.Framework;

namespace PetShelter.ViewModels
{
    public class ShelterDetailsVM : BaseVM
    {
        public ShelterDetailsVM()
        {
            this.Pets = new List<PetDetailsVM>();
            this.Employees = new List<UserDetailsVM>();
        }
        
        [Required]
        public int PetCapacity { get; set; }

        [Required]
        public int LocationId { get; set; }

        public List<UserDetailsVM> Employees { get; set; }

        public List<PetDetailsVM> Pets { get; set; }
    }
}
