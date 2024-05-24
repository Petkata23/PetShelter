namespace PetShelter.ViewModels
{
    public class PetVaccineDetailsVM : BaseVM
    {
        public int PetId { get; set; }
        public string PetName { get; set; } 
        public int VaccineId { get; set; }
        public string VaccineName { get; set; } 
        public string VaccineDescription { get; set; }
    }
}
