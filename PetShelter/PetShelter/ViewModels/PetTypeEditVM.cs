using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;

namespace PetShelter.ViewModels
{
    public class PetTypeEditVM : BaseVM
    {
        [Required]
        
        public string Name { get; set; }

        public IEnumerable<SelectListItem> PetTypeList { get; set; }
    }
}
