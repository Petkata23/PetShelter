using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetShelter.Data.Entities;
using PetShelter.Shared.Attributes;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Services.Contracts;

namespace PetShelter.Services.Services
{
    [AutoBind]
    public class PetsService : BaseCrudService<PetDto, IPetRepository>, IPetsService
    {
        private readonly IPetRepository _petRepository;
        private readonly IUserRepository _userRepository;
        private readonly IShelterRepository _shelterRepository;

        public PetsService(IPetRepository petRepository, IUserRepository userRepository, IShelterRepository shelterRepository)
            : base(petRepository) // Call the base constructor
        {
            _petRepository = petRepository;
            _userRepository = userRepository;
            _shelterRepository = shelterRepository;
        }

        public async Task GivePetAsync(int userId, int shelterId, PetDto petDto)
        {
            var shelter = await _shelterRepository.GetByIdAsync(shelterId);
            var user = await _userRepository.GetByIdAsync(userId);

            if (shelter == null || user == null)
            {
                throw new ArgumentException("Shelter or user not found.");
            }

            var newPet = new Pet
            {
                Name = petDto.Name,
                Age = petDto.Age,
                Color = petDto.Color,
                IsAdopted = petDto.IsAdopted,
                IsEuthanized = petDto.IsEuthanized,
                PetTypeId = petDto.PetTypeId,
                BreedId = petDto.BreedId,
                GiverId = userId,
                ShelterId = shelterId,
                PetVaccines = petDto.PetVaccines.Select(v => new PetVaccine { VaccineId = v.VaccineId }).ToList()
            };

            // Convert newPet to PetDto
            var newPetDto = new PetDto
            {
                Name = newPet.Name,
                Age = newPet.Age,
                Color = newPet.Color,
                IsAdopted = newPet.IsAdopted,
                IsEuthanized = newPet.IsEuthanized,
                PetTypeId = newPet.PetTypeId,
                BreedId = newPet.BreedId,
                GiverId = newPet.GiverId,
                ShelterId = newPet.ShelterId,
                PetVaccines = newPet.PetVaccines.Select(v => new PetVaccineDto { VaccineId = v.VaccineId }).ToList()
            };

            await _petRepository.CreateAsync(newPetDto);
        }


        public async Task AdoptPetAsync(int userId, int petId)
        {
            var pet = await _petRepository.GetByIdAsync(petId);
            var user = await _userRepository.GetByIdAsync(userId);

            if (pet == null || user == null)
            {
                throw new ArgumentException("Pet or user not found.");
            }

            pet.AdopterId = userId;
            pet.IsAdopted = true;

            await _petRepository.UpdateAsync(pet);
        }

        public async Task<IEnumerable<PetDto>> GetAllActiveAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
