namespace MinimalEndpoint.Demo.Endpoints.HelloWorld
{
    public class HelloWorldEndpoint : IEndpoint
    {
        public static string Template => "/";

        public static HttpMethod Method => HttpMethod.Get;

        public static Delegate Handle => () => Results.Ok("Hello World");

        public static Action<RouteHandlerBuilder>? RouteHandlerBuilderAction =>
            (builder) => builder.AllowAnonymous();

    }

}
