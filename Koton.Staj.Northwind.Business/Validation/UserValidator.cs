
using FluentValidation;
using Koton.Staj.Northwind.Entities.Concrete;

namespace Koton.Staj.Northwind.Business.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Username)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
                .MinimumLength(6).WithMessage("Kullanıcı en az 6 karakter olmalıdır.")
                .MaximumLength(20).WithMessage("Kullanıcı adı en fazla 20 karakter olmalıdır.");

            
            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Parola boş olamaz.")
                .MinimumLength(6).WithMessage("Parola en az 6 karakter olmalıdır.")
                .MaximumLength(10).WithMessage("Parola en fazla 10 karakter olmalıdır.")
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d).+$") 
                .WithMessage("Parola hem harf (alfabetik karakter) hem de sayı içermelidir.");

        }
    }
}
