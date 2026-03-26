using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ContestSystem.API.DTOs;

public class RegisterUserDto
{


    [Required]
    public string Name { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public ContestType Role { get; set; }

    public enum ContestType
    {
        VIP,
        Normal,
        Guest
    }
}