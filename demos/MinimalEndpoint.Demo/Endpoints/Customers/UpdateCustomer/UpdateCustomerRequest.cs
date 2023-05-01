namespace MinimalEndpoint.Demo.Endpoints.Customers.UpdateCustomer;
public record UpdateCustomerRequest
{
    public int Id { get; set; } 
    public string Name { get; set; } = string.Empty;

}
