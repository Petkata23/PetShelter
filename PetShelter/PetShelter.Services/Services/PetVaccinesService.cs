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
    public class PetVaccinesService : BaseCrudService<PetVaccineDto, IPetVaccineRepository>, IPetVaccinesService
    {
        public PetVaccinesService(IPetVaccineRepository repository) : base(repository)
        {

        }
    }
}
