using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShelter.Controllers;
using PetShelter.Services.Services;
using PetShelter.Shared;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;
using PetShelter.ViewModels;
using System.Security.Claims;

public class PetController : BaseCrudController<PetDto, IPetRepository, IPetsService, PetEditVM, PetDetailsVM>
{
    private readonly IPetTypesService _petTypeService; 
    private readonly IBreedsService _breedService;
    private readonly ISheltersService _shelterService;
    private readonly IVaccinesService _vaccineService;
    private readonly IPetsService _petsService;
    private readonly IUsersService _usersService;

    public PetController(IPetsService service, IBreedsService breedsService, IPetTypesService petTypeService, ISheltersService shelterService, IVaccinesService vaccineService, IUsersService usersService, IMapper mapper)
        : base(service, mapper)
    {
        _petTypeService = petTypeService; 
        _breedService = breedsService;
        _shelterService = shelterService;
        _vaccineService = vaccineService;
        _petsService = service;
        _usersService = usersService;
    }

    protected override async Task<PetEditVM> PrePopulateVMAsync(PetEditVM editVM)
    {
        editVM.PetTypeList = (await _petTypeService.GetAllAsync())
            .Select(x => new SelectListItem($"{x.Name}", x.Id.ToString()));
            editVM.BreedList = (await _breedService.GetAllActiveAsync())
                .Select(x => new SelectListItem($"{x.Name}, {x.Size}", x.Id.ToString()));
        editVM.ShelterList = (await _shelterService.GetAllAsync())
            .Select(x => new SelectListItem($"{x.PetCapacity}, {x.LocationId}", x.Id.ToString()));
        editVM.VaccineList = (await _vaccineService.GetAllAsync())
            .Select(x => new SelectListItem($"{x.Name}", x.Id.ToString()));

        return editVM;
    }


    [HttpGet]
    public virtual async Task<IActionResult> GivePet()
    {
        var editVM = await PrePopulateVMAsync(new PetEditVM());

        return View(editVM);
    }

    [HttpPost]
    public async Task<IActionResult> GivePet(int userId, int shelterId, PetEditVM editVM)
    {
        try
        {
            string loggedUsername = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await this._usersService.GetByUsernameAsync(loggedUsername);
            var model = this._mapper.Map<PetDto>(editVM);
            await _petsService.GivePetAsync(user.Id, shelterId, model);
            return await List();
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


    [HttpGet]
    public async Task<IActionResult> AdoptPet(int petId)
    {
        try
        {
            string loggedUsername = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await this._usersService.GetByUsernameAsync(loggedUsername);
            await _petsService.AdoptPetAsync(user.Id, petId);
            return await List();
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

    [HttpGet]
    public virtual async Task<IActionResult> VaccinatePet()
    {
        var editVM = await PrePopulateVMAsync(new PetEditVM());

        return View(editVM);
    }

    [HttpPost]
    public async Task<IActionResult> VaccinatePet(int petId, int vaccineId)
    {
        try
        {
            var vaccine = await this._vaccineService.GetByIdIfExistsAsync(vaccineId);
            await _petsService.VaccinatePetAsync(vaccineId, petId);
            return await List();
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
