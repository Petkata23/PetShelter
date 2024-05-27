using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShelter.Services.Services;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;
using PetShelter.ViewModels;

namespace PetShelter.Controllers
{
    public class ShelterController : BaseCrudController<ShelterDto, IShelterRepository, ISheltersService, ShelterEditVM, ShelterDetailsVM>
    {
        private readonly ILocationsService _locationsService;

        public ShelterController(ISheltersService service, ILocationsService locationsService, IMapper mapper)
            : base(service, mapper)
        {
            _locationsService = locationsService;
        }

        protected override async Task<ShelterEditVM> PrePopulateVMAsync()
        {
            var locations = await _locationsService.GetAllActiveAsync();
            var editVM = new ShelterEditVM
            {
                LocationsList = (await _locationsService.GetAllActiveAsync())
                    .Select(x => new SelectListItem($"{x.Country}, {x.City}, {x.Address}", x.Id.ToString()))
            };

            return editVM;
        }
    }
}
