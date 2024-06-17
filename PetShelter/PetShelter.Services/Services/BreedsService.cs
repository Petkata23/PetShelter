using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShelter.Shared.Attributes;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;

namespace PetShelter.Services.Services
{
    [AutoBind]
    public class BreedsService : BaseCrudService<BreedDto, IBreedRepository>, IBreedsService
    {
        public BreedsService(IBreedRepository repository) : base(repository)
        {

        }

        public async Task<IEnumerable<BreedDto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
