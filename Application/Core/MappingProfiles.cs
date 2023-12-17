using Application.DTOs.Catalogues;
using Application.DTOs.Tables;
using Domain.Models.Catalogues;
using Domain.Models.Tables;

namespace Application.Core
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<Event, EventDto>();
            CreateMap<EventDto, Event>()
                .ForMember(x => x.TableEvents, y => y.Ignore())
                .ForMember(x => x.CreatedAt, y => y.Ignore())
                .ForMember(x => x.UpdatedAt, y => y.Ignore());

            CreateMap<EventCategory, EventCategoryDto>();
            CreateMap<EventCategoryDto, EventCategory>()
                .ForMember(x => x.Events, y => y.Ignore())
                .ForMember(x => x.CreatedAt, y => y.Ignore())
                .ForMember(x => x.UpdatedAt, y => y.Ignore());

            CreateMap<EventType, EventTypeDto>();
            CreateMap<EventTypeDto, EventType>()
                .ForMember(x => x.Events, y => y.Ignore())
                .ForMember(x => x.CreatedAt, y => y.Ignore())
                .ForMember(x => x.UpdatedAt, y => y.Ignore());

            CreateMap<TicketType, TicketTypeDto>();
            CreateMap<TicketTypeDto, TicketType>()
                .ForMember(x => x.Tickets, y => y.Ignore())
                .ForMember(x => x.CreatedAt, y => y.Ignore())
                .ForMember(x => x.UpdatedAt, y => y.Ignore());
        }
    }
}
