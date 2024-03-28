namespace PetShelter.Shared.Dtos
{
    public class LocationDto : BaseModel
    {
        public string City { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public int? ShelterId { get; set; }

        public ShelterDto Shelter { get; set; }
    }
}
