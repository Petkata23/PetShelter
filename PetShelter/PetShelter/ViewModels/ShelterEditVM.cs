using Microsoft.Build.Framework;

namespace PetShelter.ViewModels
{
    public class ShelterEditVM : BaseVM
    {
        [Required]
        public int PetCapacity { get; set; }
        [Required]
        public int LocationId { get; set; }
        [Required]
        public LocationDetailsVM Location { get; set; }
    }
}
