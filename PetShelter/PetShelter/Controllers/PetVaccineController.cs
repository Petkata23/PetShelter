using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShelter.Services.Services;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;
using PetShelter.ViewModels;

namespace PetShelter.Controllers
{
    public class PetVaccineController : BaseCrudController<PetVaccineDto, IPetVaccineRepository, IPetVaccinesService, PetVaccineTypeEditVM, PetVaccineDetailsVM>
    {
        private readonly IPetsService _petService;
        private readonly IVaccinesService _vaccineService;

        public PetVaccineController(IPetVaccinesService service, IPetsService petService, IVaccinesService vaccineService, IMapper mapper) : base(service, mapper) 
        {
            _petService = petService;
            _vaccineService = vaccineService;
        }

        protected override async Task<PetVaccineTypeEditVM> PrePopulateVMAsync(PetVaccineTypeEditVM editVM)
        {

            editVM.PetList = (await _petService.GetAllActiveAsync())
                .Select(x => new SelectListItem($"{x.Name}", x.Id.ToString()));
            editVM.VaccineList = (await _vaccineService.GetAllAsync())
                .Select(x => new SelectListItem($"{x.Name}", x.Id.ToString()));
            

            return editVM;
        }
    }
}
