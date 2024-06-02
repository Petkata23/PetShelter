using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShelter.Controllers;
using PetShelter.Services.Services;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;
using PetShelter.ViewModels;

public class PetController : BaseCrudController<PetDto, IPetRepository, IPetsService, PetEditVM, PetDetailsVM>
{
    private readonly IPetTypesService _petTypeService; 
    private readonly IBreedsService _breedService;
    private readonly ISheltersService _shelterService;
    private readonly IVaccinesService _vaccineService;
    private readonly IPetsService _petsService;

    public PetController(IPetsService service, IBreedsService breedsService, IPetTypesService petTypeService, ISheltersService shelterService, IVaccinesService vaccineService, IMapper mapper)
        : base(service, mapper)
    {
        _petTypeService = petTypeService; 
        _breedService = breedsService;
        _shelterService = shelterService;
        _vaccineService = vaccineService;
        _petsService = service;
    }

    protected override async Task<PetEditVM> PrePopulateVMAsync(PetEditVM editVM)
    {
        new PetEditVM
        {
            PetTypeList = (await _petTypeService.GetAllActiveAsync())
                .Select(x => new SelectListItem($"{x.Name}", x.Id.ToString())),
            BreedList = (await _breedService.GetAllActiveAsync())
                .Select(x => new SelectListItem($"{x.Name}, {x.Size}", x.Id.ToString())),
            ShelterList = (await _shelterService.GetAllActiveAsync())
                .Select(x => new SelectListItem($"{x.PetCapacity}, {x.LocationId}", x.Id.ToString())),
            VaccineList = (await _vaccineService.GetAllActiveAsync())
                .Select(x => new SelectListItem($"{x.Name}", x.Id.ToString()))
        };

        return editVM;
    }


    [HttpPost]
    public async Task<IActionResult> CreatePet(int userId, int shelterId, [FromBody] PetDto petDto)
    {
        try
        {
            await _petsService.GivePetAsync(userId, shelterId, petDto);
            return Ok(); // Можеш да върнеш и друг response код, в зависимост от успешността на операцията
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AdoptPet(int userId, int petId)
    {
        try
        {
            await _petsService.AdoptPetAsync(userId, petId);
            return Ok(); // Можеш да върнеш и друг response код, в зависимост от успешността на операцията
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
