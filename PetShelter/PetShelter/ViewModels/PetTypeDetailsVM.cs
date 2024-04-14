namespace PetShelter.ViewModels
{
    public class PetTypeDetailsVM : BaseVM
    {
        public PetTypeDetailsVM()
        {
            Pets = new List<PetDetailsVM>();
        }

        public string Name { get; set; }

        private List<PetDetailsVM> _pets = new List<PetDetailsVM>();

        public ICollection<PetDetailsVM> Pets
        {
            get => _pets;
            set => _pets = new List<PetDetailsVM>(value);
        }

    }
}
