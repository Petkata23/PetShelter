namespace PetShelter.ViewModels
{
    public class PetVaccineDetailsVM : BaseVM
    {
        public int PetId { get; set; }
        public PetDetailsVM Pet { get; set; }
        public int VaccineId { get; set; }
        public VaccineDetailsVM Vaccine { get; set; }
    }
}
