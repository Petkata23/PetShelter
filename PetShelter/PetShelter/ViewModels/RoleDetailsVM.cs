namespace PetShelter.ViewModels
{
    public class RoleDetailsVM : BaseVM
    {
        public RoleDetailsVM()
        {
            this.Users = new List<UserDetailsVM>();
        }

        public string Name { get; set; }

        public List<UserDetailsVM> Users { get; set; }
    }
}
