using AutoMapper;
using PetShelter.Data.Entities;
using PetShelter.Shared.Dtos;

namespace PetShelter
{
    internal class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Breed, BreedDto>().ReverseMap();
        }
    }
}
