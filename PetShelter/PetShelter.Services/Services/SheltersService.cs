using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;

namespace PetShelter.Services.Services
{
    public class SheltersService : BaseCrudService<ShelterDto, IShelterRepository>, ISheltersService
    {
        public SheltersService(IShelterRepository repository) : base(repository)
        {

        }

        public async Task<IEnumerable<ShelterDto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
