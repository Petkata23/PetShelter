using AutoMapper;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;
using PetShelter.ViewModels;

namespace PetShelter.Controllers
{
    public class PetController : BaseCrudController<PetDto, IPetRepository, IPetTypeService, PetEditVM, PetDetailsVM>
    {
        public PetController(IPetTypeService service, IMapper mapper) : base(service, mapper) 
        {

        }
    }
}
