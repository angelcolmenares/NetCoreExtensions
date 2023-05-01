namespace MinimalEndpoint.Demo.Endpoints.Customers.CreateCustomer;
public record CreateCustomerRequest
{
    public string Name { get; set; } = default!;
}
