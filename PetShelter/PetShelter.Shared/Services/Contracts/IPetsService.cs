using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShelter.Services.Contracts;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;

namespace PetShelter.Shared.Services.Contracts
{
    public interface IPetsService : IBaseCrudService<PetDto, IPetRepository>
    {
        Task<IEnumerable<PetDto>> GetAllActiveAsync();
        Task GivePetAsync(int userId, int shelterId, PetDto pet);
        Task AdoptPetAsync(int userId, int petId);
    }
}
