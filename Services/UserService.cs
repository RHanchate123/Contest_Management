using Contest_Management.API.Exceptions;
using Contest_Management.API.Interfaces;
using ContestSystem.API.DTOs;
using IdentityApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Text.RegularExpressions;
using System.Transactions;

namespace Contest_Management.Services
{
    public partial class UserService : IUserInterface
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _signInManager = signInManager;
        }

        #region API Methods
        [EnableRateLimiting("writeLimiter")]
        [HttpPost]
        public async Task<IResult> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            if(registerUserDto == null || string.IsNullOrEmpty(registerUserDto.Name)
                                            || string.IsNullOrEmpty(registerUserDto.Email)
                                            || string.IsNullOrEmpty(registerUserDto.Password))
            {
                return Results.BadRequest(
                   new
                   {
                       Message ="Invalid User Data, Please ensure that Name, " +
                                "Email and Password fields are valid."
                   });
            }

            //validate email is correct.
            if (!regexEmail().IsMatch(registerUserDto.Email)) throw new APIBusinessExceptions("Email is not valid.");

            if (registerUserDto.Role == null)
                throw new Exception("User Role missing");

            var role = registerUserDto.Role.ToString();

            var user = new ApplicationUser()
            {
                Name = registerUserDto.Name,
                Email = registerUserDto.Email,
                UserName = registerUserDto.Email,
                NormalizedEmail = registerUserDto.Email.ToUpper()
            };

            //Create an identity User
            var createUser = await _userManager.CreateAsync(user, registerUserDto.Password);

            if(!createUser.Succeeded && createUser.Errors.Count() > 0)
                throw new APIBusinessExceptions(string.Format("Unable to create the user having email: {}", createUser.Errors.Select(e => e.Description).FirstOrDefault()));

            //Create user role
            var addRoleToUserResult = await _userManager.AddToRoleAsync(user, role);
                if (!addRoleToUserResult.Succeeded) 
                    throw new APIBusinessExceptions(string.Format("Unable to create role for the user {0}. Please try again later.", registerUserDto.Name));

            scope.Complete();

            return Results.Ok(new { Message = "User created successfully!" });

        }

        [EnableRateLimiting("readLimiter")]
        [HttpPost]
        public async Task<ApplicationUser> LoginAsync(LoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(
                dto.Email,      
                dto.Password,
                false,          // rememberMe
                false           // lockoutOnFailure
            );

            if (!result.Succeeded)
                throw new APIBusinessExceptions("Unable to login, please check username and password and try again.");

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                throw new APIBusinessExceptions("User does not exists");

            return user;
        }
        #endregion


        #region Regex
        [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        private static partial Regex regexEmail();
        #endregion
    }
}
