using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Billings;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Billings.Delete;

public class DeleteBillingUseCase : IDeleteBillingUseCase
{
    private readonly IBillingsWriteOnlyRepository _repository;
    private readonly IUnityOfWork _unityOfWork;

    public DeleteBillingUseCase(
        IBillingsWriteOnlyRepository repository,
        IUnityOfWork unityOfWork
    )
    {
        _repository = repository;
        _unityOfWork = unityOfWork;
    }

    public async Task Execute(long id)
    {
        var response = await _repository.Delete(id);

        if (!response)
        {
            throw new NotFoundException(ResourceErrorMessages.BILLING_NOT_FOUND);
        }

        await _unityOfWork.Commit();
    }
}
