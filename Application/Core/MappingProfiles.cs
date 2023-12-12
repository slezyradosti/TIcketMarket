using Application.DTOs;
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
        }
    }
}
