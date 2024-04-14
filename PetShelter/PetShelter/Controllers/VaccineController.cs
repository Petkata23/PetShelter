using AutoMapper;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;
using PetShelter.ViewModels;

namespace PetShelter.Controllers
{
    public class VaccineController : BaseCrudController<VaccineDto, IVaccineRepository, IVaccinesService, VaccineEditVM, VaccineDetailsVM>
    {
        public VaccineController(IVaccinesService service, IMapper mapper) : base(service, mapper) 
        {

        }
    }
}
