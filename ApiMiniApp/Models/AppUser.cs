using System.Security.Principal;
using Microsoft.AspNetCore.Identity;

namespace ApiMiniApp.Models;

public class AppUser : IdentityUser
{
    public string FullName { get; set; } = null!;
}