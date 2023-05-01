namespace MinimalEndpoint.Demo.Endpoints.Customers.GetCustomers;
public class GetCustomersEndpoint : IEndpoint
{
    public static string Template => GetTemplate<GetCustomersEndpoint>("api");

    public static HttpMethod Method => HttpMethod.Post;

    public static Delegate Handle => HandleRequest;

    public static Action<RouteHandlerBuilder>? RouteHandlerBuilderAction
        => b => b.WithTags(GetTag<GetCustomersEndpoint>());

    private static async Task<Ok<GetCustomersReponse>> HandleRequest(
    GetCustomersRequest request,
    HttpContext httpContext,
    GetCustomersHandler service,
    CancellationToken cancellationToken = default)
    {
        var response = await service.Handle(request, cancellationToken);
        return TypedResults.Ok(response);
    }
}
