using ApiMiniApp.Data;
using ApiMiniApp.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiMiniApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(AppDbContext context, IMapper mapper) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await context.AppUsers
            .ProjectTo<UserReturnDto>(mapper.ConfigurationProvider)
            .ToListAsync());
    }
}