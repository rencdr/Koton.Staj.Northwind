using AutoMapper;
using Koton.Staj.Northwind.Entities;
using Koton.Staj.Northwind.Entities.Dtos;

namespace Koton.Staj.Northwind.Business.Mapper
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {

            CreateMap<Cart, DisplayCartDto>()
 .ForMember(dest => dest.TotalCartAmount, opt => opt.MapFrom(src => src.Quantity * src.Product.UnitPrice))
 .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
 .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.UnitPrice))
 .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Categories.CategoryName))
 .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Categories.Description));

        }
    }
}
