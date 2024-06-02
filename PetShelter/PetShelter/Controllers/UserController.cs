using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShelter.Services.Services;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;
using PetShelter.ViewModels;

namespace PetShelter.Controllers
{
    public class UserController : BaseCrudController<UserDto, IUserRepository, IUsersService, UserEditVM, UserDetailsVM>
    {
        private readonly IRolesService _roleService;
        private readonly ISheltersService _shelterService;

        public UserController(IUsersService service, IRolesService roleService, ISheltersService shelterService, IMapper mapper) : base(service, mapper) 
        {
            _roleService = roleService;
            _shelterService = shelterService;
        }

        protected override async Task<UserEditVM> PrePopulateVMAsync(UserEditVM editVM)
        {
            editVM.RoleList = (await _roleService.GetAllActiveAsync())
                    .Select(x => new SelectListItem($"{x.Name}", x.Id.ToString()));
            editVM.ShelterList = (await _shelterService.GetAllActiveAsync())
                    .Select(x => new SelectListItem($"{x.PetCapacity}, {x.Location}", x.Id.ToString()));

            return editVM;
        }
    }
}
