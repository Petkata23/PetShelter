namespace PetShelter.ViewModels
{
    public class VaccineDetailsVM : BaseVM
    {
        public VaccineDetailsVM()
        {
            PetVaccines = new List<PetVaccineDetailsVM>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<PetVaccineDetailsVM> PetVaccines { get; set; }
    }
}
