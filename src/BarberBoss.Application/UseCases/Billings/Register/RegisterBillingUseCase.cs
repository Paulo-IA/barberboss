using AutoMapper;
using BarberBoss.Application.AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Billings;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Billings.Register;

public class RegisterBillingUseCase : IRegisterBillingUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnityOfWork _unityOfWork;
    private readonly IBillingsWriteOnlyRepository _repository;

    public RegisterBillingUseCase(
        IMapper mapper,
        IUnityOfWork unityOfWork,
        IBillingsWriteOnlyRepository repository
    )
    {
        _mapper = mapper;
        _unityOfWork = unityOfWork;
        _repository = repository;
    }

    public async Task<ResponseRegisteredBillingJson> Execute(RequestBillingJson request)
    {
        Validate(request);

        var entity = _mapper.Map<Billing>(request);

        await _repository.Add(entity);
        
        await _unityOfWork.Commit();

        return _mapper.Map<ResponseRegisteredBillingJson>(entity);
    }

    private void Validate(RequestBillingJson request)
    {
        var validator = new RegisterBillingValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
