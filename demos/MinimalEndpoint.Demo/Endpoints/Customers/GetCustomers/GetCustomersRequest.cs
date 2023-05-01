namespace MinimalEndpoint.Demo.Endpoints.Customers.GetCustomers;

public record GetCustomersRequest
{
    public List<int> IdList { get; set; } = new List<int>();  
}
