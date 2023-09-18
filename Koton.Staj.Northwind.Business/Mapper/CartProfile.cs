using AutoMapper;
using Koton.Staj.Northwind.Data.DataUtilities;
using Koton.Staj.Northwind.Entities.Concrete;
using Koton.Staj.Northwind.Entities.Dtos;

public class CartProfile : Profile
{
    public CartProfile()
    {  
        CreateMap<Cart, DisplayCartDto>()
            .ForMember(dest => dest.TotalCartAmount, opt => opt.MapFrom(src => src.Quantity * src.UnitPrice))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        CreateMap<ResponseModel<List<Cart>>, ResponseModel<List<DisplayCartDto>>>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));
    }
}


