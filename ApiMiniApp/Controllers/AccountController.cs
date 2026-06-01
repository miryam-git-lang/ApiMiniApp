using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiMiniApp.Data;
using ApiMiniApp.Dtos;
using ApiMiniApp.Models;
using ApiMiniApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ApiMiniApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(
    UserManager<AppUser> userManager,
    IEmailService emailService,
    IJwtService jwtService,
    AppDbContext context) : ControllerBase
{
    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound();

        var roles = await userManager.GetRolesAsync(user);
        return Ok(new { user.FullName, user.UserName, user.Email, Roles = roles });
    }

    [HttpGet("confirm-email")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound("User not found");

        var result = await userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
            return BadRequest("Email confirmation failed");

        return Ok("Email confirmed successfully");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var user = new AppUser()
        {
            FullName = registerDto.FullName,
            UserName = registerDto.Username,
            Email = registerDto.Email,
        };

        var identityResult = await userManager.CreateAsync(user, registerDto.Password);
        if (!identityResult.Succeeded)
            return BadRequest(identityResult.Errors);

        await userManager.AddToRoleAsync(user, "member");

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        var confirmLink = Url.Action(
            action: "ConfirmEmail",
            controller: "Account",
            values: new { userId = user.Id, token },
            protocol: Request.Scheme
        );

        if (confirmLink == null)
            return StatusCode(500, "Could not generate confirmation link");

        await emailService.SendEmailAsync(
            user.Email,
            "Confirm your email",
            $"Please confirm your email by clicking <a href='{confirmLink}'>here</a>."
        );

        return Ok("User registered successfully. Please check your email to confirm your account.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await userManager.FindByNameAsync(loginDto.UserNameOrEmail)
                   ?? await userManager.FindByEmailAsync(loginDto.UserNameOrEmail);

        if (user == null)
            return Unauthorized("Invalid username or password.");

        if (!user.EmailConfirmed)
            return BadRequest("Please confirm your email before logging in.");

        var passwordValid = await userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!passwordValid)
            return BadRequest("Invalid username or password.");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("FullName", user.FullName),
        };
        var tokenString = await jwtService.GenerateJwt(user, claims);
        var refreshToken = await jwtService.GenerateRefreshJwt(user);
        return Ok(new { Token = tokenString, RefreshToken = refreshToken });
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
            return Unauthorized();

        var result = await userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok("Password changed successfully.");
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user == null || !(await userManager.IsEmailConfirmedAsync(user)))
            return Ok("If an account with that email exists, a password reset link has been sent.");

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        return Ok(new
        {
            token = token,
            email = dto.Email
        });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            return Ok("If an account with that email exists, your password has been reset.");

        var result = await userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
        if (result.Succeeded)
            return Ok("Password reset successfully.");

        return BadRequest(result.Errors);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto dto)
    {
        var refreshToken = await context.RefreshTokenSettings
            .FirstOrDefaultAsync(rt => rt.Token == dto.RefreshToken);

        if (refreshToken == null || refreshToken.Expires < DateTime.Now)
            return Unauthorized("Invalid or expired refresh token");

        var user = await userManager.FindByIdAsync(refreshToken.UserId);
        if (user == null)
            return Unauthorized("User not found");

        context.RefreshTokenSettings.Remove(refreshToken);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("FullName", user.FullName),
        };

        var newToken = await jwtService.GenerateJwt(user, claims);
        var newRefreshToken = await jwtService.GenerateRefreshJwt(user);

        return Ok(new { Token = newToken, RefreshToken = newRefreshToken });
    }
}