using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel;

namespace PetShelter.ViewModels
{
    public class ShelterEditVM : BaseVM
    {
        [Required]
        public int PetCapacity { get; set; }

        [Required]
        [DisplayName("Location")]
        public int LocationId { get; set; }

        public IEnumerable<SelectListItem> LocationsList { get; set; }
    }
}
