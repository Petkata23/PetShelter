using AutoMapper;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;
using PetShelter.ViewModels;

namespace PetShelter.Controllers
{
    public class PetVaccineController : BaseCrudController<PetVaccineDto, IPetVaccineRepository, IPetVaccinesService, PetVaccineTypeEditVM, PetVaccineDetailsVM>
    {
        public PetVaccineController(IPetVaccinesService service, IMapper mapper) : base(service, mapper) 
        {

        }
    }
}
