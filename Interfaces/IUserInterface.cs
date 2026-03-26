using ContestSystem.API.DTOs;
using IdentityApplication.Models;

namespace Contest_Management.API.Interfaces
{
    public interface IUserInterface
    {
        //To store webhook 1 data and create a identity user
        public Task<IResult> RegisterUserAsync(RegisterUserDto registerUserDto);

        //To allow user to login, if otp entered is valid.
        public Task<ApplicationUser> LoginAsync(LoginDto loginDto);

    }
}
