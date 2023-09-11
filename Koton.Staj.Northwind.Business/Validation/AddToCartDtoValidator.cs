using FluentValidation;
using Koton.Staj.Northwind.Entities.Dtos;

namespace Koton.Staj.Northwind.Business.Validation
{
    public class AddToCartDtoValidator : AbstractValidator<AddToCartDto>
    {
        public AddToCartDtoValidator()
        {
            RuleFor(cartItem => cartItem.UserId)
                .NotEmpty().WithMessage("Kullanıcı kimliği boş bırakılamaz.")
                .GreaterThan(0).WithMessage("Kullanıcı kimliği geçerli olmalıdır.");

            RuleFor(cartItem => cartItem.ProductId)
                .NotEmpty().WithMessage("Ürün kimliği boş bırakılamaz.")
                .GreaterThan(0).WithMessage("Ürün kimliği geçerli olmalıdır.")
                .LessThanOrEqualTo(70).WithMessage("Ürün kimliği 70'den büyük olmamalıdır.");

            RuleFor(cartItem => cartItem.Quantity)
                .NotEmpty().WithMessage("Ürün miktarı boş bırakılamaz.")
                .GreaterThan(0).WithMessage("Ürün miktarı 0'dan büyük olmalıdır.")
                .LessThanOrEqualTo(5).WithMessage("Ürün miktarı 5'ten büyük olmamalıdır.");

        }
    }
}
