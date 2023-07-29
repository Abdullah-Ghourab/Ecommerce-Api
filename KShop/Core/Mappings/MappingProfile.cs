using AutoMapper;
using KShop.Core.DTOs;
using KShop.Core.Models;

namespace KShop.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<ProductDto, Product>();
            CreateMap<CartDto, Cart>();
        }
    }
}
