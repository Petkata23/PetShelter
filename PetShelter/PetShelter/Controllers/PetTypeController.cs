using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShelter.Services.Services;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;
using PetShelter.ViewModels;

namespace PetShelter.Controllers
{

    public class PetTypeController : BaseCrudController<PetTypeDto, IPetTypeRepository, IPetTypesService, PetTypeEditVM, PetTypeDetailsVM>
    {
        private readonly IPetTypesService _petTypeService;

        public PetTypeController(IPetTypesService service, IMapper mapper) : base(service, mapper) 
        {
            _petTypeService = petTypeService;
        }

        protected override async Task<PetTypeEditVM> PrePopulateVMAsync()
        {
            var editVM = new ShelterEditVM
            {
                PetTypeList = (await _petTypeService.GetAllActiveAsync())
                    .Select(x => new SelectListItem($"{x.Country}, {x.City}, {x.Address}", x.Id.ToString()))
            };

            return editVM;
        }
    }
}
