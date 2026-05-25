using AutoMapper;
using ApiMiniApp.Models;
using ApiMiniApp.Dtos;
using ApiMiniApp.Extensions;

namespace ApiMiniApp.Profiles;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Event, EventsInOrganizerReturnDto>();
        CreateMap<EventCreateDto, Event>();
        CreateMap<Event, EventReturnDto>();
        CreateMap<EventUpdateDto, Event>();

        CreateMap<Organizer, OrganizerInEventReturnDto>();
        CreateMap<OrganizerCreateDto, Organizer>();
        CreateMap<Organizer, OrganizerReturnDto>();

        CreateMap<TicketCreateDto, Ticket>();
        CreateMap<Ticket, TicketReturnDto>();
        CreateMap<TicketUpdateDto, Ticket>();

        CreateMap<EventCreateFileDto, Event>()
            .ForMember(dest => dest.BannerImageUrl, opt => opt.MapFrom(src => src.File.SaveFile("wwwroot/images")));
        CreateMap<OrganizerCreateFileDto, Organizer>()
            .ForMember(dest => dest.LogoUrl, opt => opt.MapFrom(src => src.File.SaveFile("wwwroot/images")));

        CreateMap<AppUser, UserReturnDto>();
    }
}
