using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Billings;

namespace BarberBoss.Application.UseCases.Billings.Register;

public class RegisterBillingUseCase : IRegisterBillingUseCase
{
    private readonly IUnityOfWork _unityOfWork;
    private readonly IBillingsWriteOnlyRepository _repository;

    public RegisterBillingUseCase(
        IBillingsWriteOnlyRepository repository,
        IUnityOfWork unityOfWork
    )
    {
        _repository = repository;
        _unityOfWork = unityOfWork;
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
        
        _unityOfWork.Commit();

        return new ResponseRegisteredBillingJson {
            Title = "Billing test"
        };
    }

    private void Validate(RequestBillingJson request)
    {
        var validator = new RegisterBillingValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            // Throw error
        }
    }
}
