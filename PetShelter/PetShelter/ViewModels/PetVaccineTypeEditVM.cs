using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PetShelter.ViewModels
{
    public class PetVaccineTypeEditVM : BaseVM
    {
        [Required]
        public int VaccineId { get; set; }

        [Required]
        public int PetId { get; set; }

        public IEnumerable<SelectListItem> VaccineList { get; set; }

        public IEnumerable<SelectListItem> PetList { get; set; }
    }
}
