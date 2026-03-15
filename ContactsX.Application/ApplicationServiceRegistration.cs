using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ContactsX.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
