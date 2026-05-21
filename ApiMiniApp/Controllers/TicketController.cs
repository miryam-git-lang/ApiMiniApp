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
public class TicketController(AppDbContext context,IMapper mapper) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await context.Tickets
            .ProjectTo<TicketReturnDto>(mapper.ConfigurationProvider)
            .ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        return Ok(await context.Tickets
            .Where(x => x.Id == id)
            .ProjectTo<TicketReturnDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync()
        );
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TicketCreateDto ticketCreateDto)
    {
        var newTicket = mapper.Map<Ticket>(ticketCreateDto);
        await context.Tickets.AddAsync(newTicket);
        await context.SaveChangesAsync();
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] TicketUpdateDto ticketUpdateDto)
    {
        var existingTicket = await context.Tickets.FindAsync(id);
        if (existingTicket == null)
        {
            return NotFound();
        }
        
        mapper.Map(ticketUpdateDto, existingTicket);
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] TicketUpdateDto ticketUpdateDto)
    {
        var existingTicket = await context.Tickets.FindAsync(id);
        if (existingTicket == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(ticketUpdateDto.Type))
        {
            existingTicket.Type = ticketUpdateDto.Type;
            existingTicket.UpdatedAt = DateTime.Now;
            await context.SaveChangesAsync();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute]int id)
    {
        var ticket = await context.Tickets.FindAsync(id);
      
        if(ticket == null)
        {
            return NotFound();
        }
        context.Tickets.Remove(ticket);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
