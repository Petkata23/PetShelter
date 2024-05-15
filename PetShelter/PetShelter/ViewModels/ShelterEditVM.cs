using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IEnumerable<SelectListItem> LocationsList { get; internal set; }
    }
}
