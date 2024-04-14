using AutoMapper;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;
using PetShelter.ViewModels;

namespace PetShelter.Controllers
{
    public class BreedController : BaseCrudController<BreedDto, IBreedRepository, IBreedsService, BreedEditVM, BreedDetailsVM>
    {
        public BreedController(IBreedsService service, IMapper mapper) : base(service, mapper)
        {

        }
    }
}
