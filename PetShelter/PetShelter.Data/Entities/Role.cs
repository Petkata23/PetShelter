namespace PetShelter.Data.Entities
{
    public class Role : BaseEntity
    {
        public Role()
        {
            this.Users = new List<User>();
        }
        
        public string Name { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
