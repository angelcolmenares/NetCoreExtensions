namespace MinimalEndpoint.Demo.Endpoints.Customers.UpdateCustomer;

public class UpdateCustomerEndpoint : IEndpoint
{
    public static string Template => GetTemplate<UpdateCustomerEndpoint>("api", "{id:int}");

    public static HttpMethod Method => HttpMethod.Put;

    public static Delegate Handle => HandleRequest;

    public static Action<RouteHandlerBuilder>? RouteHandlerBuilderAction =>
        builder => builder.WithTags(GetTag<UpdateCustomerEndpoint>());

    private static async Task<Ok<UpdateCustomerResponse>> HandleRequest(
    int id,
    UpdateCustomerRequest request,
    HttpContext httpContext,
    UpdateCustomerHandler service,
    CancellationToken cancellationToken = default)
    {
        var response = await service.Handle(request, cancellationToken);
        return TypedResults.Ok(response);
    }
}

//https://khalidabuhakmeh.com/static-abstract-members-in-csharp-11-interfaces