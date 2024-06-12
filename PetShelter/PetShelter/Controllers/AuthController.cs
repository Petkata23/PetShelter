using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShelter.Shared.Services.Contracts;
using PetShelter.ViewModels;
using PetShelter.Shared;
using PetShelter.Shared.Dtos;
using PetShelter.Shared.Security;

namespace PetShelter.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IRolesService rolesService;
        private readonly IMapper mapper;

        public AuthController(
            IUsersService usersService,
            IRolesService rolesService,
            IMapper mapper)
        {
            this.usersService = usersService;
            this.rolesService = rolesService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginVM model)
        {
            string loggedUsername = User.FindFirst(ClaimTypes.Name)?.Value;

            if (loggedUsername != null)
            {
                return Forbid();
            }

            if (!await this.usersService.CanUserLoginAsync(model.Username, model.Password))
            {
                return BadRequest(Constants.InvalidCredentials);
            }

            await LoginUser(model.Username);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> LoginUser(string username)
        {
            // Retrieve the user by username
            var user = await this.usersService.GetByUsernameAsync(username);

            // Check if the user exists
            if (user != null)
            {
                // Create claims for the user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    // Check if the user has a role
                    user.Role != null ? new Claim(ClaimTypes.Role, user.Role.Name) : null
                };

                // Create the identity for the user
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Create the principal
                var principal = new ClaimsPrincipal(identity);

                // Sign in the user
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal);

                // Redirect the user to the home page
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                // Handle the case where the user is not found
                // For example, you could return a BadRequest response with an error message
                return BadRequest(Constants.UserNotFound);
            }
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterVM userCreateModel)
        {
            string loggedUsername = User.FindFirst(ClaimTypes.Name)?.Value;

            if (loggedUsername != null)
            {
                return Forbid();
            }

            if (await this.usersService.GetByUsernameAsync(userCreateModel.Username) != default)
            {
                return BadRequest(Constants.UserAlreadyExists);

            }

            var hashedPassword = PasswordHasher.HashPassword(userCreateModel.Password);
            userCreateModel.Password = hashedPassword;

            var userDto = this.mapper.Map<UserDto>(userCreateModel);
            userDto.RoleId = (await rolesService.GetByNameIfExistsAsync(UserRole.User.ToString()))?.Id;
            await this.usersService.SaveAsync(userDto);

            await LoginUser(userDto.Username);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            string loggedUsername = User.FindFirst(ClaimTypes.Name)?.Value;

            if (loggedUsername != null)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }


            return RedirectToAction(nameof(HomeController.Index), "Home");

        }

        [HttpGet]
        public IActionResult ConfirmLogout()
        {
            return View();
        }

    }
}
