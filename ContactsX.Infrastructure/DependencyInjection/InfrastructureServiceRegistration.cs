using ContactsX.Application.Interfaces.Repositories;
using ContactsX.Application.Interfaces.Services;
using ContactsX.Infrastructure.Repositories;
using ContactsX.Infrastructure.Mappings;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ContactsX.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IEntityService, EntityService>();
        services.AddAutoMapper(cfg => cfg.AddProfile<EntityProfile>());

        return services;
    }
}