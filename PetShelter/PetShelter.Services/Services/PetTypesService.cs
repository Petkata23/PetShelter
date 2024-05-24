using System.Collections.Generic;
using System.Threading.Tasks;
using PetShelter.Shared.Attributes;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;

namespace PetShelter.Services.Services
{
    [AutoBind]
    public class PetTypesService : BaseCrudService<PetTypeDto, IPetTypeRepository>, IPetTypesService
    {
<<<<<<< Updated upstream
        public PetTypesService(IPetTypeRepository repository) : base(repository)
        {
=======
        

        public PetTypesService(IPetTypeRepository repository) : base(repository)
        {
            
        }
>>>>>>> Stashed changes

        }
    }
}
