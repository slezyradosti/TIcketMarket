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

            CreateMap<EventTable, EventTableDto>();
            CreateMap<EventTableDto, EventTable>()
                .ForMember(x => x.TableEvents, y => y.Ignore())
                .ForMember(x => x.CreatedAt, y => y.Ignore())
                .ForMember(x => x.UpdatedAt, y => y.Ignore());

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>()
                .ForMember(x => x.TicketOrders, y => y.Ignore())
                .ForMember(x => x.CreatedAt, y => y.Ignore())
                .ForMember(x => x.UpdatedAt, y => y.Ignore());

            CreateMap<Ticket, TicketDto>();
            CreateMap<TicketDto, Ticket>()
                .ForMember(x => x.TicketOrders, y => y.Ignore())
                .ForMember(x => x.Number, y => y.Ignore()) // ???
                .ForMember(x => x.CreatedAt, y => y.Ignore())
                .ForMember(x => x.UpdatedAt, y => y.Ignore());

            CreateMap<TicketOrder, TicketOrderDto>();
            CreateMap<TicketOrderDto, TicketOrder>()
                .ForMember(x => x.Order, y => y.Ignore())
                .ForMember(x => x.Ticket, y => y.Ignore())
                .ForMember(x => x.CreatedAt, y => y.Ignore())
                .ForMember(x => x.UpdatedAt, y => y.Ignore());
            
            CreateMap<TableEvent, TableEventDto>();
            CreateMap<TableEventDto, TableEvent>()
                .ForMember(x => x.Event, y => y.Ignore())
                .ForMember(x => x.Table, y => y.Ignore())
                .ForMember(x => x.CreatedAt, y => y.Ignore())
                .ForMember(x => x.UpdatedAt, y => y.Ignore());
            
            CreateMap<TicketDiscount, TicketDiscountDto>();
            CreateMap<TicketDiscountDto, TicketDiscount>()
                .ForMember(x => x.Tickets, y => y.Ignore())
                .ForMember(x => x.CreatedAt, y => y.Ignore())
                .ForMember(x => x.UpdatedAt, y => y.Ignore());
        }
    }
}
