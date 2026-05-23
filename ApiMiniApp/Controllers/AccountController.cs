using System.Security.Claims;
using ApiMiniApp.Dtos;
using ApiMiniApp.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiMiniApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager) : Controller
{

    [HttpPost("register")]
    public async Task <IActionResult> Register([FromBody] RegisterDto registerDto)
    {

       var user = new AppUser()
       {
           FullName = registerDto.FullName,
           UserName = registerDto.Username,
           Email = registerDto.Email,
       };
       
       var identityResult = await userManager.CreateAsync(user, registerDto.Password);
       if (!identityResult.Succeeded)
       {
           return BadRequest();
       }
        await userManager.AddToRoleAsync(user, "member");
        return Ok("Account created successfully!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await userManager.FindByNameAsync(loginDto.UserName);
        if (user == null)
        {
            return Unauthorized();
        }
        var passwordValid = await userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!passwordValid)
        {
            return BadRequest("Invalid username or password.");
        }
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("FullName", user.FullName),
            
        };
        var roles = await userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        return Ok("Logged in!");
    }
    
}