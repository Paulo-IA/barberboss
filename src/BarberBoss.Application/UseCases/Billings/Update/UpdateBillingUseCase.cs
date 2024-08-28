using AutoMapper;
using BarberBoss.Application.UseCases.Billings.Register;
using BarberBoss.Communication.Requests;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Billings;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Billings.Update;

public class UpdateBillingUseCase : IUpdateBillingUseCase
{
    private readonly IBillingUpdateOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnityOfWork _unityOfWork;

    public UpdateBillingUseCase(
        IBillingUpdateOnlyRepository repository,
        IMapper mapper,
        IUnityOfWork unityOfWork
    )
    {
        _repository = repository;
        _mapper = mapper;
        _unityOfWork = unityOfWork;
    }

    public async Task Execute(RequestBillingJson request, long id)
    {
        Validate(request);

        var billing = await _repository.GetById(id);

        if (billing is null)
        {
            throw new NotFoundException(ResourceErrorMessages.BILLING_NOT_FOUND);
        }

        _mapper.Map(request, billing);

        _repository.Update(billing);

        await _unityOfWork.Commit();
    }

    private void Validate(RequestBillingJson request)
    {
        var validator = new BillingValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
