using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Billings;

namespace BarberBoss.Application.UseCases.Billings.Register;

public class RegisterBillingUseCase : IRegisterBillingUseCase
{
    private readonly IBillingsWriteOnlyRepository _repository;

    public RegisterBillingUseCase(IBillingsWriteOnlyRepository repository)
    {
        _repository = repository;
    }

    public ResponseRegisteredBillingJson Execute(RequestBillingJson request)
    {
        Validate(request);

        var entity = new Billing
        {
            Title = request.Title,
            Description = request.Description,
            Date = request.Date,
            Amount = request.Amount,
            PaymentType = (Domain.Enums.PaymentType)request.PaymentType,
        };

        _repository.Add(entity);
        

        return new ResponseRegisteredBillingJson {
            Title = "Billing test"
        };
    }

    private void Validate(RequestBillingJson request)
    {
        
    }
}
