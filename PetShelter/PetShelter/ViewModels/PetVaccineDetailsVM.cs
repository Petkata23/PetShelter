using Microsoft.AspNetCore.Mvc.Rendering;

namespace PetShelter.ViewModels
{
    public class PetVaccineDetailsVM : BaseVM
    {
        public int VaccineId { get; set; }

        public int PetId { get; set; }

        public IEnumerable<SelectListItem> VaccineList { get; set; }

        public IEnumerable<SelectListItem> PetList { get; set; }

        public VaccineDetailsVM Vaccine { get; set; }

        public PetDetailsVM Pet { get; set; }

    }
}
