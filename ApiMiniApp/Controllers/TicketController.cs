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
        return Ok(context.Organizers
            .ProjectTo<TicketReturnDto>(mapper.ConfigurationProvider)
            .ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(context.Organizers
            .Where(x => x.Id == id)
            .ProjectTo<TicketReturnDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync()
        );
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TicketCreateDto ticketCreateDto)
    {
        var newTicket = mapper.Map<Ticket>(ticketCreateDto);
        context.Tickets.Add(newTicket);
        await context.SaveChangesAsync();
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] TicketUpdateDto ticketUpdateDto)
    {
        if (id != ticketUpdateDto.Id)
        {
            return BadRequest();
        }
        var existingTicket = await context.Tickets.FindAsync(id);
        if (existingTicket == null)
        {
            return NotFound();
        }
        
        mapper.Map(ticketUpdateDto, existingTicket);
        await context.SaveChangesAsync();
        return NoContent();
        
        
    }
}
