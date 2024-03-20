namespace PetShelter.Data.Entities
{
    public class PetVaccine : BaseEntity
    {
        public int PetId { get; set; }

        public virtual Pet Pet { get; set; }

        public int VaccineId { get; set; }

        public virtual Vaccine Vaccine { get; set; }
    }
}
