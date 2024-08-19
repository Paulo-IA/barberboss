using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Billings;
public interface IBillingsWriteOnlyRepository
{
    void Add(Billing billing);
}
