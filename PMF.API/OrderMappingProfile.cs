using AutoMapper;
using PMF.API.Entities;
using PMF.API.Models;

namespace PMF.API
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Part, PartDto>()
                .ForMember(d => d.Surface, c => c.MapFrom(p => p.SurfaceId == (int)SurfaceType.Painted
                ? SurfaceType.Painted.ToString() : SurfaceType.Galvanised.ToString()))
                .ForMember(d => d.ActualStorage, c => c.MapFrom(p => p.Storages.FirstOrDefault().Name))
                .ForMember(d => d.DestinationStorage, c => c.MapFrom(p => p.Storages.LastOrDefault().Name));

            CreateMap<Order, OrderDto>()
                .ForMember(d => d.Parts, c => c.MapFrom(p => p.Parts));
        }
    }
}
