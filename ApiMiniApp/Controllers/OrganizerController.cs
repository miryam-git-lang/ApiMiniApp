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
    public async Task<ActionResult> Get([FromRoute]int id)
    {
        return Ok(await context.Organizers
            .Where(x => x.Id == id)
            .ProjectTo<OrganizerReturnDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync()
        );
    }
    [HttpPost]
    public async Task<ActionResult> Post([FromForm] OrganizerCreateDto organizerCreateDto)
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
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute]int id, [FromForm] OrganizerUpdateDto organizerUpdateDto)
    {
        var existingorganizer = await context.Organizers.FindAsync(id);
        if (existingorganizer == null)
        {
            return NotFound();
        }
        if(existingorganizer.Name != organizerUpdateDto.Name && await context.Organizers.AnyAsync(e => e.Name == organizerUpdateDto.Name))
        {
            return BadRequest("An organizer with the same name already exists.");
        }
        
        mapper.Map(organizerUpdateDto, existingorganizer);
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch([FromRoute]int id, [FromForm] OrganizerUpdateDto organizerUpdateDto)
    {
        var existingorganizer = await context.Organizers.FindAsync(id);
        if (existingorganizer == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(organizerUpdateDto.Name))
        {
            existingorganizer.Name = organizerUpdateDto.Name;
            existingorganizer.UpdatedAt = DateTime.Now;
            await context.SaveChangesAsync();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute]int id)
    {
        var organizer = await context.Organizers.FindAsync(id);
      
        if(organizer == null)
        {
            return NotFound();
        }
        context.Organizers.Remove(organizer);
        await context.SaveChangesAsync();
        return NoContent();
    }
    [HttpPost("{id}/logoUrl")]
    public async Task<IActionResult> AddFileToorganizer([FromRoute]int id, [FromForm]OrganizerCreateFileDto organizerCreateFileDto)
    {
        var organizer = await context.Organizers.FindAsync(id);
        if (organizer == null)
        {
            return NotFound();
        }
        if(organizer.LogoUrl != null)
        {
            return BadRequest("This organizer already has a banner image.");
        }
      
        mapper.Map(organizerCreateFileDto, organizer);
        await context.SaveChangesAsync();
        return NoContent();
    }
    [HttpGet("{organizerId}/events")]
    public async Task<ActionResult> GetEventsByOrganizer([FromRoute]int organizerId)
    {
        var organizerExist = await context.Organizers.AnyAsync(e => e.Id == organizerId);
        if (!organizerExist)
        {
            return NotFound();
        }

        var eventsInOrganizer = context.Events.Where(t => t.OrganizerId == organizerId)
            .ProjectTo<EventReturnDto>(mapper.ConfigurationProvider);
        
        return Ok(eventsInOrganizer);
    }
    
    [HttpPost("{organizerId}/logo")]
    public async Task<ActionResult> AddFileToOrganizerById([FromRoute]int organizerId, [FromForm] OrganizerCreateFileDto organizerCreateFileDto)
    {
        var organizerEntity = await context.Organizers.FindAsync(organizerId);
        if (organizerEntity == null)
        {
            return NotFound();
        }
        if(organizerEntity.LogoUrl != null)
        {
            return BadRequest("This organizer already has a banner image.");
        }
      
        mapper.Map(organizerCreateFileDto, organizerEntity);
        await context.SaveChangesAsync();
        return NoContent();
    }
    
}