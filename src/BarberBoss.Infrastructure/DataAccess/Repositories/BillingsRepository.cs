using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Billings;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess.Repositories;

internal class BillingsRepository : IBillingsWriteOnlyRepository, IBillingsReadOnlyRepository, IBillingUpdateOnlyRepository
{
    private readonly BarberBossDbContext _dbContext;
    public BillingsRepository(BarberBossDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Billing billing)
    {
        await _dbContext.Billings.AddAsync(billing);

    }
    
    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Billings.FirstOrDefaultAsync(b => b.Id == id);
        if (result is null)
        {
            return false;
        }

        _dbContext.Billings.Remove(result);
        
        return true;
    }

    public async Task<List<Billing>> GetAll()
    {
        return await _dbContext.Billings.AsNoTracking().ToListAsync();
    }
    
    async Task<Billing?> IBillingsReadOnlyRepository.GetById(long id)
    {
        return await _dbContext.Billings.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
    }
    async Task<Billing?> IBillingUpdateOnlyRepository.GetById(long id)
    {
        return await _dbContext.Billings.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
    }

    public void Update(Billing billing)
    {
        _dbContext.Billings.Update(billing);
    }
}
