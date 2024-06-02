using System.Threading.Tasks;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Security;
using PetShelter.Shared.Services.Contracts;

namespace PetShelter.Services.Services
{
    public class UsersService : BaseCrudService<UserDto, IUserRepository>, IUsersService
    {
        private readonly IUserRepository userRepository;

        public UsersService(IUserRepository repository) : base(repository)
        {
            userRepository = repository;
        }

        public async Task<bool> CanUserLoginAsync(string username, string password)
        {
            var user = await _repository.GetUserByUsernameAsync(username);

            if (user == null)
            {
                return false;
            }

            bool passwordMatches = PasswordHasher.VerifyPassword(password, user.Password);

            return passwordMatches;
        }

        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            return await userRepository.GetUserByUsernameAsync(username);
        }

        public async Task<UserDto> GetByUsernameAsync(string username)
        {
            return await userRepository.GetByUsernameAsync(username);
        }
    }
}