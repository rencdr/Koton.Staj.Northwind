using AutoMapper;
using Koton.Staj.Northwind.Entities.Concrete;
using Koton.Staj.Northwind.Entities.Dtos;

namespace Koton.Staj.Northwind.Business.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.UnitsInStock, opt => opt.MapFrom(src => src.UnitsInStock))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        }

    }

}
