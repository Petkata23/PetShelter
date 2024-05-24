using AutoMapper;
<<<<<<< Updated upstream
=======
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShelter.Services.Services;
>>>>>>> Stashed changes
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;
using PetShelter.ViewModels;

namespace PetShelter.Controllers
{
    public class ShelterController : BaseCrudController<ShelterDto, IShelterRepository, ISheltersService, ShelterEditVM, ShelterDetailsVM>
    {
<<<<<<< Updated upstream
        public ShelterController(ISheltersService service, IMapper mapper) : base(service, mapper) 
=======
        private readonly ILocationsService _locationsService;

        public ShelterController(ISheltersService service, ILocationsService locationsService, IMapper mapper)
            : base(service, mapper)
>>>>>>> Stashed changes
        {
        }
    }
}
