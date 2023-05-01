namespace MinimalEndpoint.Demo.Endpoints.Customers.GetCustomer;
public class GetCustomerEndpoint : IEndpoint
{
    public static string Template => GetTemplate<GetCustomerEndpoint>("api", "{id:int}");

    public static HttpMethod Method => HttpMethod.Get;

    public static Delegate Handle => HandleRequest;

    public static Action<RouteHandlerBuilder>? RouteHandlerBuilderAction
        => b => b.WithTags(GetTag<GetCustomerEndpoint>());

   
    private static async Task<Ok<GetCustomerResponse>> HandleRequest(
    int id,    
    HttpContext httpContext,
    GetCustomerHandler service,
    CancellationToken cancellationToken = default)
    {
        var response = await service.Handle(new GetCustomerRequest { Id= id} , cancellationToken);
        return TypedResults.Ok(response);
    }
}
