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
}