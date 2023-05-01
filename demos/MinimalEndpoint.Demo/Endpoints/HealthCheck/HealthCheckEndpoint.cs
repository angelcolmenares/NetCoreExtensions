namespace MinimalEndpoint.Demo.Endpoints.HealthCheck
{
    public class HealthCheckEndpoint : IEndpoint
    {
        public static string Template => "/hc";

        public static HttpMethod Method => HttpMethod.Get;

        public static Delegate Handle => (IWebHostEnvironment env) =>
        {
            var assembly = typeof(Program).Assembly;
            var info = assembly.GetAssemblyInfo(env);
            return Results.Ok(info.ToString().Replace("\r", "").Replace("\n", ""));
        };

        public static Action<RouteHandlerBuilder>? RouteHandlerBuilderAction => default;
    }
}
