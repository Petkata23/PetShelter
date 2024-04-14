using Microsoft.Build.Framework;

namespace PetShelter.ViewModels
{
    public class ShelterDetailsVM : BaseVM
    {
        public ShelterDetailsVM()
        {
            Pets = new List<PetDetailsVM>();
            Employees = new List<UserDetailsVM>();
        }
        
        [Required]
        public int PetCapacity { get; set; }

        [Required]
        public int LocationId { get; set; }

        public LocationDetailsVM Location { get; set; }

        public ICollection<UserDetailsVM> Employees { get; set; }

        public ICollection<PetDetailsVM> Pets { get; set; }
    }
}
