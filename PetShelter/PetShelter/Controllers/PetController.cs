using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShelter.Controllers;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;
using PetShelter.ViewModels;

public class PetController : BaseCrudController<PetDto, IPetRepository, IPetsService, PetEditVM, PetDetailsVM>
{
    private readonly IPetTypesService _petTypeService; 
    private readonly IBreedsService _breedService;
    private readonly ISheltersService _shelterService;

    public PetController(IPetsService service, IBreedsService breedsService, IPetTypesService petTypeService, ISheltersService shelterService, IMapper mapper)
        : base(service, mapper)
    {
        _petTypeService = petTypeService; 
        _breedService = breedsService;
        _shelterService = shelterService;
    }

    protected override async Task<PetEditVM> PrePopulateVMAsync()
    {
        var editVM = new PetEditVM
        {
            PetTypeList = (await _petTypeService.GetAllActiveAsync())
                .Select(x => new SelectListItem($"{x.Name}", x.Id.ToString())),
            BreedList = (await _breedService.GetAllActiveAsync())
                .Select(x => new SelectListItem($"{x.Name}, {x.Size}", x.Id.ToString())),
            ShelterList = (await _shelterService.GetAllActiveAsync())
                .Select(x => new SelectListItem($"{x.PetCapacity}, {x.LocationId}", x.Id.ToString()))
        };

        return editVM;
    }
}
