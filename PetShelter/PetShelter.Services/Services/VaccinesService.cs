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
    public class VaccinesService : BaseCrudService<VaccineDto, IVaccineRepository>, IVaccinesService
    {
        public VaccinesService(IVaccineRepository repository) : base(repository)
        {

        }

        public async Task<IEnumerable<VaccineDto>> GetAllActiveAsync()
        {

            return await _repository.GetAllAsync();

        }
    }
}
