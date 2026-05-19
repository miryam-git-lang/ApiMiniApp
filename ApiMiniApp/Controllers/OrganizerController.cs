using ApiMiniApp.Data;
using ApiMiniApp.Dtos;
using ApiMiniApp.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiMiniApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizerController(AppDbContext context, IMapper mapper) : Controller
{
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        return Ok(await context.Organizers
            .ProjectTo<OrganizerReturnDto>(mapper.ConfigurationProvider)
            .ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        return Ok(await context.Organizers
            .Where(x => x.Id == id)
            .ProjectTo<OrganizerReturnDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync()
        );
    }
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] OrganizerCreateDto organizerCreateDto)
    {
        if (await context.Organizers.AnyAsync(o => o.Name == organizerCreateDto.Name))
        {
            return BadRequest("An organizer with the same name already exists.");
        }
        var newOrganizer = mapper.Map<Organizer>(organizerCreateDto);
        context.Organizers.Add(newOrganizer);
        await context.SaveChangesAsync();
        return Created();
    }
}