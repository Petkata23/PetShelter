namespace PetShelter.ViewModels
{
    public class PetDetailsVM : BaseVM
    {
        public PetDetailsVM()
        {
            PetVaccines = new List<PetVaccineDetailsVM>();
            Adopters = new List<UserDetailsVM>();
            Givers = new List<UserDetailsVM>();
            Shelters = new List<ShelterDetailsVM>();
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Color { get; set; }
        public bool IsAdopted { get; set; }
        public bool IsEuthanized { get; set; }
        public int PetTypeId { get; set; }
        public PetTypeDetailsVM PetType { get; set; }
        public int BreedId { get; set; }
        public BreedDetailsVM Breed { get; set; }
        public List<UserDetailsVM> Adopters { get; set; }
        public List<UserDetailsVM> Givers { get; set; }
        public List<ShelterDetailsVM> Shelters { get; set; }
        public List<PetVaccineDetailsVM> PetVaccines { get; set; }
    }
}
