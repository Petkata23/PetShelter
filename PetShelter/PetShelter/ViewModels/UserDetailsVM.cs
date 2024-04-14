namespace PetShelter.ViewModels
{
    public class UserDetailsVM : BaseVM
    {
        public UserDetailsVM()
        {
            AdoptedPets = new List<PetDetailsVM>();
            GivenPets = new List<PetDetailsVM>();
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? RoleId { get; set; }

        public RoleDetailsVM Role { get; set; }

        public int? ShelterId { get; set; }

        public ShelterDetailsVM Shelter { get; set; }

        public ICollection<PetDetailsVM> AdoptedPets { get; set; }

        public ICollection<PetDetailsVM> GivenPets { get; set; }
    }
}
