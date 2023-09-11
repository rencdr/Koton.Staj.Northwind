using AutoMapper;
using Koton.Staj.Northwind.Entities;

namespace Koton.Staj.Northwind.Business.Mapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Cart, UserOrder>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UserAddress, opt => opt.Ignore()) 
                .ForMember(dest => dest.UserPhoneNumber, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) 
                .ForMember(dest => dest.OrderId, opt => opt.Ignore()) 
                .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.CartId)); 


        }
    }
}
