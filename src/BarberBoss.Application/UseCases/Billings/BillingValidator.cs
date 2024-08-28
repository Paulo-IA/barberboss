using BarberBoss.Communication.Requests;
using BarberBoss.Exception;
using FluentValidation;

namespace BarberBoss.Application.UseCases.Billings;

public class BillingValidator : AbstractValidator<RequestBillingJson>
{
    public BillingValidator()
    {
        RuleFor(billing => billing.Title).NotEmpty().WithMessage(ResourceErrorMessages.TITLE_REQUIRED);

        RuleFor(billing => billing.Amount).GreaterThan(0).WithMessage(ResourceErrorMessages.THE_AMOUNT_MUST_BE_GREATHER_THAN_ZERO);

        RuleFor(billing => billing.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessages.BILLING_CANT_BE_FOR_THE_FUTURE);

        RuleFor(billing => billing.PaymentType).IsInEnum().WithMessage(ResourceErrorMessages.PAYMENT_TYPE_INVALID);
    }
}
