namespace MinimalEndpoint.Demo.Endpoints.Customers.CreateCustomer
{
    public class CreateCustomerEndpoint : IEndpoint
    {
        public static string Template => GetTemplate<CreateCustomerEndpoint>("api");

        public static HttpMethod Method => HttpMethod.Post;

        public static Delegate Handle => HandleRequest;

        public static Action<RouteHandlerBuilder>? RouteHandlerBuilderAction
            => b => b.WithTags(GetTag<CreateCustomerEndpoint>());

       
        private static async Task<Ok<CreateCustomerResponse>> HandleRequest(
        CreateCustomerRequest request,
        HttpContext httpContext,
        CreateCustomerHandler service,
        CancellationToken cancellationToken = default)
        {
            var response = await service.Handle(request, cancellationToken);
            return TypedResults.Ok(response);
        }
    }
}
