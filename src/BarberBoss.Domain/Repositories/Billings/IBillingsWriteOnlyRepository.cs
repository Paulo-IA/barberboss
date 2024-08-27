﻿using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Billings;
public interface IBillingsWriteOnlyRepository
{
    Task Add(Billing billing);

    /// <summary>
    /// This function returns TRUE if deletion was successful
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> Delete(long id);
}
