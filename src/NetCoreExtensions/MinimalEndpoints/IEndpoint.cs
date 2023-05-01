
using Microsoft.AspNetCore.Builder;

namespace NetCoreExtensions.MinimalEndpoints;

public interface IEndpoint
{
    static abstract string Template { get; }
    static abstract HttpMethod Method { get; }
    static abstract Delegate Handle { get; }

    static abstract Action<RouteHandlerBuilder>? RouteHandlerBuilderAction { get; }

}
