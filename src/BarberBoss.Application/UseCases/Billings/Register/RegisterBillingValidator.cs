using BarberBoss.Communication.Requests;
using FluentValidation;

namespace BarberBoss.Application.UseCases.Billings.Register;

public class RegisterBillingValidator : AbstractValidator<RequestBillingJson>
{
    public RegisterBillingValidator()
    {
        RuleFor(billing => billing.Title).NotEmpty().WithMessage("The title is required!");
        RuleFor(billing => billing.Amount).GreaterThan(0).WithMessage("The amount must be grather than zero!");
        RuleFor(billing => billing.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("The billing can't be for the future!");
        RuleFor(billing => billing.PaymentType).IsInEnum().WithMessage("Payment type is not valid!");
    }
}
