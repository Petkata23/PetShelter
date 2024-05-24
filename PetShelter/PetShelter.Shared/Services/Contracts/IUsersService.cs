using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShelter.Services.Contracts;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Repos.Contracts;
using PetShelter.Shared.Security;

namespace PetShelter.Shared.Services.Contracts
{
    public interface IUsersService : IBaseCrudService<UserDto, IUserRepository>
    {
        Task<bool> CanUserLoginAsync(string username, string password);
        Task<UserDto> GetByUsernameAsync(string username);
        Task<UserDto> GetUserByUsernameAsync(string username);
        
    }
}
