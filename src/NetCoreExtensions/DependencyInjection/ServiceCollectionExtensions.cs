using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace NetCoreExtensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, Assembly assembly)
    {
        services.AddServicesFromAssembly<IScoped>(assembly);
        services.AddServicesFromAssembly<ITransient>(assembly);
        services.AddServicesFromAssembly<ISingleton>(assembly);

        return services;
    }
    private static IServiceCollection AddServicesFromAssembly<TScope>(this IServiceCollection services, Assembly assembly)
    {
        var types = assembly
           .GetTypes()
           .Where(p => p.IsClass && !p.IsAbstract && p.IsAssignableTo(typeof(TScope)))
           .Select(f => new { ImplementationType = f, Interfaces = f.GetInterfaces().Where(f => f.Name != typeof(TScope).Name).ToList() })
           .ToList();

        var serviceLifetime = GetServiceLifetime<TScope>();
        foreach (var type in types)
        {
            var injectableProperties = GetProtectedInjectableProperties(type.ImplementationType);

            if (type.Interfaces.Count == 0)
            {
                services.Add(
                    injectableProperties.Count > 0
                    ? new ServiceDescriptor(
                        type.ImplementationType,
                        sp => ResolveAndInjectProperties(type.ImplementationType, injectableProperties, sp)!,
                        serviceLifetime)
                    : new ServiceDescriptor(
                        type.ImplementationType,
                        type.ImplementationType,
                        serviceLifetime));
                continue;
            }

            services.Add(
                    injectableProperties.Count > 0
                    ? new ServiceDescriptor(
                        type.Interfaces[0],
                        sp => ResolveAndInjectProperties(type.ImplementationType, injectableProperties, sp)!,
                        serviceLifetime)
                    : new ServiceDescriptor(
                        type.Interfaces[0],
                        type.ImplementationType,
                        serviceLifetime));


            foreach (var serviceType in type.Interfaces.Skip(1))
            {
                services.Add(new ServiceDescriptor(
                    serviceType,
                    sp =>
                    {
                        var implementation = sp.GetService(type.Interfaces[0]);
                        if (implementation == null)
                        {
                            throw new ArgumentNullException(nameof(serviceType), $"Cannot resolve {type.Interfaces[0].FullName}");
                        }
                        return implementation;
                    },
                    serviceLifetime));
            }
        }
        return services;
    }

    private static object ResolveAndInjectProperties(
        Type implementationType,
        List<PropertyInfo> injectableProperties,
        IServiceProvider sp)
    {
        var constructorParams = ResolveConstructorParams(implementationType, sp);

        object instance;
        if (constructorParams?.Length == 0)
        {
            instance = Activator.CreateInstance(implementationType)!;
        }
        else
        {
            instance = Activator.CreateInstance(implementationType, constructorParams)!;
        }

        foreach (var pi in injectableProperties)
        {
            var value = sp.GetService(pi.PropertyType);
            pi.SetValue(instance, value, null);
        }

        return instance;
    }

    private static ServiceLifetime GetServiceLifetime<TScope>() =>
        typeof(TScope).Name switch
        {
            "ISingleton" => ServiceLifetime.Singleton,
            "IScoped" => ServiceLifetime.Scoped,
            "ITransient" => ServiceLifetime.Transient,
            _ => ServiceLifetime.Transient,

        };

    private static List<PropertyInfo> GetProtectedInjectableProperties(Type type)
    {
        return type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(p => p.IsDefined(typeof(InjectablePropertyAttribute)) && (p.GetMethod?.IsFamily ?? false))
            .ToList();
    }

    private static object?[] ResolveConstructorParams(Type serviceType, IServiceProvider serviceProvider)
    {
        // Get the first public constructor of the service type
        var constructor = serviceType.GetConstructors().FirstOrDefault();

        // If there is no constructor, return an empty array
        if (constructor == null) return Array.Empty<object>();

        // Get the constructor parameters
        var parameters = constructor.GetParameters();

        // Create a list to store the resolved parameters
        var resolvedParameters = new List<object?>();

        // Loop through each parameter
        foreach (var parameter in parameters)
        {
            // Try to get the service from the service provider
            var service = serviceProvider.GetService(parameter.ParameterType);

            // If the service is not null, add it to the list
            if (service != null)
            {
                resolvedParameters.Add(service);
            }
            else
            {
                // Otherwise, use the default value of the parameter type
                resolvedParameters.Add(parameter.ParameterType.IsValueType ? Activator.CreateInstance(parameter.ParameterType) : null);
            }
        }

        // Return the array of resolved parameters
        return resolvedParameters.ToArray();
    }

}
