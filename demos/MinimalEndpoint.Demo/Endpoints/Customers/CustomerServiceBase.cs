namespace MinimalEndpoint.Demo.Endpoints.Customers;

public class CustomerServiceBase
{
    [InjectableProperty]
    protected ICustomerStore CustomerStore { get; set; } = default!;
}
