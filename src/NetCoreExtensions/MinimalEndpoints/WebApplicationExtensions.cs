using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection;
using System.Net.Http;

namespace NetCoreExtensions.MinimalEndpoints;

public static class WebApplicationExtensions
{
    public static WebApplication MapEndpoint<TEndpoint>(this WebApplication app)
       where TEndpoint : IEndpoint
    {
        var routeHandlerBuilder = app.MapMethods(
            TEndpoint.Template,
            new[] { TEndpoint.Method.ToString() },
            TEndpoint.Handle);        
        TEndpoint.RouteHandlerBuilderAction?.Invoke(routeHandlerBuilder);
        return app;
    }


    public static WebApplication MapEndpoints(this WebApplication app, Assembly assembly)
    {
        var lt = DiscoverEndpoints(assembly);
        foreach( var t in lt)
        {            
            var template = t.GetProperty(nameof(IEndpoint.Template))?.GetValue(null) as string;
            var method = (t.GetProperty(nameof(IEndpoint.Method))?.GetValue(null) as HttpMethod)?.ToString();
            var handler = t.GetProperty(nameof(IEndpoint.Handle))?.GetValue(null) as Delegate;
            var routeHandlerBuilderAction = t.GetProperty(nameof(IEndpoint.RouteHandlerBuilderAction))?.GetValue(null) as Action<RouteHandlerBuilder>;

            var routeHandlerBuilder = app.MapMethods(template!, new[] { method }!, handler!);
            routeHandlerBuilderAction?.Invoke(routeHandlerBuilder);
        }
               

        return app;
    }

    private static IEnumerable<Type> DiscoverEndpoints(Assembly assembly)
    {
        return assembly
            .GetTypes()
            .Where(p => p.IsClass && !p.IsAbstract && p.IsAssignableTo(typeof(IEndpoint)))
            .Select(t =>t );
            
    }
}
