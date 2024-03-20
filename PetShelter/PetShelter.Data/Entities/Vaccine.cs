namespace PetShelter.Data.Entities
{
    public class Vaccine : BaseEntity
    {
        public Vaccine()
        {
            this.PetVaccines = new List<PetVaccine>();
        }
                
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual List<PetVaccine> PetVaccines { get; set; }
    }
}
