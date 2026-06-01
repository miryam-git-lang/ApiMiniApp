using System.Security.Claims;
using ApiMiniApp.Models;

namespace ApiMiniApp.Services;

public interface IJwtService
{
    Task<string> GenerateJwt(AppUser appUser, List<Claim> claimsList);
    Task<string> GenerateRefreshJwt(AppUser appUser);
}