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
public class EventController(AppDbContext context, IMapper mapper) : Controller
{
   [HttpGet]
   public async Task <IActionResult> Get()
   {
      return Ok(await context.Events
         .ProjectTo<EventReturnDto>(mapper.ConfigurationProvider)
         .ToListAsync());
   }

   [HttpGet("{id}")]
   public async Task<IActionResult> Get([FromRoute]int id)
   {
      return Ok(await context.Events
         .Where(x => x.Id == id)
         .ProjectTo<EventReturnDto>(mapper.ConfigurationProvider)
         .FirstOrDefaultAsync());
   }

   [HttpPost]
   public async Task <IActionResult> Post([FromBody]EventCreateDto eventCreateDto)
   {
      if(await context.Events.AnyAsync(e => e.Title == eventCreateDto.Title))
      {
         return BadRequest("An event with the same name already exists.");
      }
      var newEvent = mapper.Map<Event>(eventCreateDto);
      context.Events.Add(newEvent);
      await context.SaveChangesAsync();
      return Created();
   }

   [HttpPut("{id}")]
   public async Task<IActionResult> Put(int id, [FromForm] EventUpdateDto eventUpdateDto)
   {
      if (id != eventUpdateDto.Id)
      {
         return BadRequest();
      }
      var existingEvent = await context.Events.FindAsync(id);
      if (existingEvent == null)
      {
         return NotFound();
      }
      if(existingEvent.Title != eventUpdateDto.Name && await context.Events.AnyAsync(e => e.Title == eventUpdateDto.Name))
      {
         return BadRequest("An event with the same name already exists.");
      }
        
      mapper.Map(eventUpdateDto, existingEvent);
      await context.SaveChangesAsync();
      return NoContent();
   }
   
   
   [HttpPatch("{id}")]
   public async Task<IActionResult> Patch(int id, [FromForm] EventUpdateDto eventUpdateDto)
   {
      var existingevent = await context.Events.FindAsync(id);
      if (existingevent == null)
      {
         return NotFound();
      }

      if (!string.IsNullOrEmpty(eventUpdateDto.Name))
      {
         existingevent.Title= eventUpdateDto.Name;
         existingevent.UpdatedAt = DateTime.Now;
         await context.SaveChangesAsync();
      }

      return NoContent();
   }

   [HttpDelete("{id}")]
   public async Task<IActionResult> Delete(int id)
   {
      var Event = await context.Events.FindAsync(id);
      
      if(Event == null)
      {
         return NotFound();
      }
      context.Events.Remove(Event);
      await context.SaveChangesAsync();
      return NoContent();
   }

   [HttpPost("{id}/banner")]
   public async Task<IActionResult> AddFileToEvent(int id, [FromForm]EventCreateFileDto eventCreateFileDto)
   {
      var Event = await context.Events.FindAsync(id);
      if (Event == null)
      {
         return NotFound();
      }
      if(Event.BannerImageUrl != null)
      {
         return BadRequest("This event already has a banner image.");
      }
      
      mapper.Map(eventCreateFileDto, Event);
      await context.SaveChangesAsync();
      return NoContent();
   }
   
   [HttpGet("{eventId}/tickets")]
   public async Task<ActionResult> GetTicketsByEvent(int eventId)
   {
      var eventExist = await context.Events.AnyAsync(e => e.Id == eventId);
      if (!eventExist)
      {
         return NotFound();
      }
      
      var ticketsInEvent = await context.Tickets.Where(t => t.EventId == eventId)
         .ProjectTo<TicketReturnDto>(mapper.ConfigurationProvider)
         .ToListAsync();
      
      return Ok(ticketsInEvent);
   }
   
   [HttpPost("{eventId}/tickets")]
   public async Task<ActionResult> PostTicketsByEvent(int eventId, [FromForm] TicketCreateDto eventCreateDto)
   {
      var eventExist = await context.Events.AnyAsync(e => e.Id == eventId);
      if (!eventExist)
      {
         return NotFound();
      }
      
      var newEvent = mapper.Map<Event>(eventCreateDto);
      context.Events.Add(newEvent);
      await context.SaveChangesAsync();
      return Created();
   }
   [HttpPost("{eventId}/banner")]
   public async Task<ActionResult> AddFileToEventById(int eventId, [FromForm] EventCreateFileDto eventCreateFileDto)
   {
      var eventEntity = await context.Events.FindAsync(eventId);
      if (eventEntity == null)
      {
         return NotFound();
      }
      if(eventEntity.BannerImageUrl != null)
      {
         return BadRequest("This event already has a banner image.");
      }
      
      mapper.Map(eventCreateFileDto, eventEntity);
      await context.SaveChangesAsync();
      return NoContent();
   }
   
   
}