using AutoMapper;
using PetShelter.Data.Entities;
using PetShelter.Shared.Attributes;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.Data.Repos
{
    [AutoBind]
    public class UserRepository : BaseRepository<User, UserDto>, IUserRepository
    {
        public UserRepository(PetShelterDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        private readonly List<UserDto> users = new List<UserDto>
        {
            new UserDto { Username = "user1", Password = "password1" },
            new UserDto { Username = "user2", Password = "password2" }
            // Add more users as needed
        };

        public async Task<UserDto> GetByUsernameAsync(string username)
        {
            return await Task.FromResult(users.FirstOrDefault(u => u.Username == username));
        }

        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            return await Task.FromResult(users.FirstOrDefault(u => u.Username == username));
        }
    }

}
