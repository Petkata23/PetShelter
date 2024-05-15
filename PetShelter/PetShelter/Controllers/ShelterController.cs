using AutoMapper;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;
using PetShelter.ViewModels;

namespace PetShelter.Controllers
{
    public class ShelterController : BaseCrudController<ShelterDto, IShelterRepository, ISheltersService, ShelterEditVM, ShelterDetailsVM>
    {
        ILocationsService _locationsService;

        protected override async Task<ShelterEditVM> PrePopulateVMAsync()
        {
            var editVM = new ShelterEditVM
            {
                LocationsList = (await _locationsService.GetAllActiveAsync())
                .Select(x => new SelectListItem($"{x.Country}, {x.City}, {x.Address}", x.Id.ToString()))
            };

            return editVM;
        }

        public ShelterController(ISheltersService service, IMapper mapper) : base(service, mapper) 
        {
           
        }
    }
}
