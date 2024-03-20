namespace PetShelter.Data.Entities
{
    public class Shelter : BaseEntity
    {
        public Shelter()
        {
            this.Pets = new List<Pet>();
            this.Employees = new List<User>();
        }

        public int PetCapacity { get; set; }

        public int LocationId { get; set; }

        public virtual Location Location { get; set; }

        public virtual List<User> Employees { get; set; }

        public virtual List<Pet> Pets { get; set; }
    }
}
