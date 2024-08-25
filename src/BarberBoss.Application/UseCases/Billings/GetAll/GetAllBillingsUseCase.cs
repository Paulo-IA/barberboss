using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories.Billings;

namespace BarberBoss.Application.UseCases.Billings.GetAll;

public class GetAllBillingsUseCase : IGetAllBillingsUseCase
{
    public readonly IBillingsReadOnlyRepository _repository;
    public GetAllBillingsUseCase(IBillingsReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseBillingsJson> Execute()
    {
        var result = await _repository.GetAll();

        return new ResponseBillingsJson
        {
            Billings = []
        };

    }
}
