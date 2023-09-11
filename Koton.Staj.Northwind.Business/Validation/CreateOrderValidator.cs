using FluentValidation;
using Koton.Staj.Northwind.Entities;
using Koton.Staj.Northwind.Entities.Dtos;

namespace Koton.Staj.Northwind.Business.Validation
{
    public class CreateOrderValidator : AbstractValidator<OrderRequestModel>
    {
        public CreateOrderValidator()
        {
            RuleFor(order => order.UserId)
                .GreaterThan(0).WithMessage("Kullanıcı kimliği geçerli olmalıdır.");

            RuleFor(order => order.UserAddress)
                .NotEmpty().WithMessage("Kullanıcı adresi boş olamaz.")
                .MaximumLength(50).WithMessage("Kullanıcı adresi en fazla 50 karakter olmalıdır.");


            RuleFor(order => order.UserPhoneNumber)
                .NotEmpty().WithMessage("Kullanıcı telefon numarası boş olamaz.")
                .Matches(@"^[0-9]+$").WithMessage("Kullanıcı telefon numarası sadece rakamlardan oluşmalıdır.");

       
        }
    }
}
