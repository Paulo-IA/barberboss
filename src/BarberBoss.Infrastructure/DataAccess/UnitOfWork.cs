﻿using BarberBoss.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DataAccess;

internal class UnitOfWork : IUnityOfWork
{
    private readonly BarberBossDbContext _dbContext;
    public UnitOfWork(BarberBossDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Commit() => _dbContext.SaveChanges();
}
