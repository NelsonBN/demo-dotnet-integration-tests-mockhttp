using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Demo.Api.Tests.Utils;

internal static class OverrideDependencyInjectionExtensions
{
    public static IServiceCollection OverrideSingleton<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    {
        services.RemoveAll(typeof(TService));
        services.AddSingleton<TService, TImplementation>();

        return services;
    }

    public static IServiceCollection OverrideScoped<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    {
        services.RemoveAll(typeof(TService));
        services.AddScoped<TService, TImplementation>();

        return services;
    }

    public static IServiceCollection OverrideTransient<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    {
        services.RemoveAll(typeof(TService));
        services.AddTransient<TService, TImplementation>();

        return services;
    }

    public static IServiceCollection Override<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    {
        var serviceType = services.SingleOrDefault(f => f.ServiceType == typeof(TService));
        if(serviceType is null)
        {
            throw new NotImplementedException($"The '{typeof(TService).Name}' was not implemented");
        }

        services.RemoveAll(serviceType.ServiceType);

        switch(serviceType.Lifetime)
        {
            case ServiceLifetime.Transient:
                services.AddTransient<TService, TImplementation>();
                break;

            case ServiceLifetime.Scoped:
                services.AddScoped<TService, TImplementation>();
                break;

            case ServiceLifetime.Singleton:
                services.AddSingleton<TService, TImplementation>();
                break;
        }

        return services;
    }
}
