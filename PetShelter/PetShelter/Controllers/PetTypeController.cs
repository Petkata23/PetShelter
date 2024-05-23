using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShelter.Services.Services;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;
using PetShelter.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace PetShelter.Controllers
{
    public class PetTypeController : BaseCrudController<PetTypeDto, IPetTypeRepository, IPetTypesService, PetTypeEditVM, PetTypeDetailsVM>
    {
        private readonly IPetTypesService _petTypeService;

        public PetTypeController(IPetTypesService service, IMapper mapper) : base(service, mapper)
        {
            _petTypeService = service;
        }

        protected override async Task<PetTypeEditVM> PrePopulateVMAsync()
        {
            var editVM = new PetTypeEditVM
            {
                PetTypeList = (await _petTypeService.GetAllActiveAsync())
                    .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                    .ToList()
            };

            return editVM;
        }
    }
}