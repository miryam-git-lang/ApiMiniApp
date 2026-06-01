using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class RegisterVm
{
    [Required(ErrorMessage = "Full name is required")]
    public string FullName { get; set; } = null!;
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = null!;
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    [Required(ErrorMessage = "Please confirm your password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string RePassword { get; set; } = null!;
}