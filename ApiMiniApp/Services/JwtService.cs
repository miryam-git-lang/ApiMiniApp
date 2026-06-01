using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiMiniApp.Data;
using ApiMiniApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ApiMiniApp.Services;

public class JwtService(
    AppDbContext context,
    IOptions<JwtSetting> jwtSettings,
    UserManager<AppUser> userManager) : IJwtService
{
    public async Task<string> GenerateJwt(AppUser appUser, List<Claim> claimsList)
    {
        var roles = await userManager.GetRolesAsync(appUser);
        claimsList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        var jwtSetting = jwtSettings.Value;
        var keys = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key));
        var creds = new SigningCredentials(keys, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: jwtSetting.Issuer,
            audience: jwtSetting.Audience,
            claims: claimsList,
            expires: DateTime.Now.AddMinutes(jwtSetting.Expire),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public async Task<string> GenerateRefreshJwt(AppUser appUser)
    {
        var refreshTokenValidity = DateTime.Now.AddMinutes(jwtSettings.Value.Expire);
        var refreshTokenEntity = new RefreshTokenSetting()
        {
            UserId = appUser.Id,
            Token = Guid.NewGuid().ToString(),
            Expires = refreshTokenValidity,
        };
            
        context.RefreshTokenSettings.Add(refreshTokenEntity);
        await context.SaveChangesAsync();
            
        return refreshTokenEntity.Token;
    }
}