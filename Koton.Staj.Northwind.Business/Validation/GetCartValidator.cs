using FluentValidation;

public class GetCartValidator : AbstractValidator<int>
{
    public GetCartValidator()
    {
        RuleFor(userId => userId)
            .NotEmpty().WithMessage("Kullanıcı kimliği boş bırakılamaz.")
            .GreaterThan(0).WithMessage("Kullanıcı kimliği 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(99999).WithMessage("Kullanıcı kimliği 99999'dan küçük veya eşit olmalıdır.");
    }
}
