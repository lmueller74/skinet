using Core.Entities;
using AutoMapper;
using API.Data;

namespace API.Helperrs
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
           CreateMap<Product, ProductToReturnDto>()
           .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
           .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
           .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
        }
    }
}