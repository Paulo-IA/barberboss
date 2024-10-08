﻿using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Billings;
using BarberBoss.Infrastructure.DataAccess;
using BarberBoss.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IBillingsWriteOnlyRepository, BillingsRepository>();
        services.AddScoped<IBillingsReadOnlyRepository, BillingsRepository>();
        services.AddScoped<IBillingUpdateOnlyRepository, BillingsRepository>();

        services.AddScoped<IUnityOfWork, UnitOfWork>();
    }

    private static void AddDbContext(
        IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("Connection"); ;

        var serverVersion = new MySqlServerVersion(new Version(8, 0, 39));

        services.AddDbContext<BarberBossDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
