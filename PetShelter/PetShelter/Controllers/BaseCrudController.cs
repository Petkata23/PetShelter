using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetShelter.Services.Contracts;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.ViewModels;

namespace PetShelter.Controllers
{
    public abstract class BaseCrudController<TModel, TRepository, TService, TEditVM, TDetailsVM> : Controller
        where TModel : BaseModel
        where TRepository : IBaseRepository<TModel>
        where TService : IBaseCrudService<TModel, TRepository>
        where TEditVM : BaseVM
        where TDetailsVM : BaseVM
    {
        protected readonly TService _service;
        protected readonly IMapper _mapper;

        protected BaseCrudController(TService service, IMapper mapper)
        {
            this._service = service;
            _mapper = mapper;
        }
    }
}
