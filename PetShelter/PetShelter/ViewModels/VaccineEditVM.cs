using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;

namespace PetShelter.ViewModels
{
    public class VaccineEditVM : BaseVM
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }

        public IEnumerable<SelectListItem> VaccineList { get; set; }

        public IEnumerable<SelectListItem> PetVaccinesTypeList { get; set; }
    }
}
