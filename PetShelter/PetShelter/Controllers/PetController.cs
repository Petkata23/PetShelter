using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using PetShelter.Services.Services;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;
using PetShelter.ViewModels;

namespace PetShelter.Controllers
{
    public class PetController : BaseCrudController<PetDto, IPetRepository, IPetTypeService, PetEditVM, PetDetailsVM>
    {
        private readonly IPetTypeService _petTypeService;
        private readonly IBreedsService _breedService;

        public PetController(IPetTypeService service, IBreedsService breedsService, IMapper mapper) : base(service, mapper)
        {
            _petTypeService = service;
            _breedService = breedsService;
        }

        protected override async Task<PetEditVM> PrePopulateVMAsync()
        {
            var editVM = new PetEditVM
            {
                PetTypeList = (await _petTypeService.GetAllActiveAsync())
                    .Select(x => new SelectListItem($"{x.Name}", x.Id.ToString())), 
                BreedList = (await _breedService.GetAllActiveAsync())
                    .Select(x => new SelectListItem($"{x.Name}, {x.Size}", x.Id.ToString())),

            };

            return editVM;
        }
    }
}