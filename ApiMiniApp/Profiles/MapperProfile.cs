using AutoMapper;
using ApiMiniApp.Models;
using ApiMiniApp.Dtos;
namespace ApiMiniApp.Profiles;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Event, EventsInOrganizerReturnDto>();
        CreateMap<Event, EventCreateDto>();
        CreateMap<Event, EventReturnDto>();
        
        CreateMap<Organizer, OrganizerInEventReturnDto>();
        CreateMap<Organizer,OrganizerCreateDto>();
        CreateMap<Organizer, OrganizerReturnDto>();
        
        CreateMap<Ticket, TicketCreateDto>();
        CreateMap<Ticket, TicketReturnDto>();
        CreateMap<Ticket, TicketUpdateDto>();
        

    }
}