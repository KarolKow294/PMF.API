using AutoMapper;
using PMF.API.Entities;
using PMF.API.Models;
using PMF.API.Services;

namespace PMF.API
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile(QrCodeService qrCodeService)
        {
            CreateMap<Part, PartDto>()
                .ForMember(d => d.Surface, c => c.MapFrom(p => p.SurfaceId == (int)SurfaceType.Painted
                ? SurfaceType.Painted.ToString() : SurfaceType.Galvanised.ToString()))
                .ForMember(d => d.QrDataImage, c => c.MapFrom(p => qrCodeService.GenerateQrCode(p.QrCodeData)))
                .ForMember(d => d.ActualStorage, c => c.MapFrom(p => p.Storages.FirstOrDefault().Name))
                .ForMember(d => d.DestinationStorage, c => c.MapFrom(p => p.Storages.LastOrDefault().Name));

            CreateMap<CreatePartDto, Part>();

            CreateMap<Order, OrderDto>()
                .ForMember(d => d.Parts, c => c.MapFrom(p => p.Parts));
        }
    }
}
