namespace MinimalEndpoint.Demo.Endpoints.Customers.GetCustomers;

public class GetCustomersHandler : ITransient
{
    public async Task<GetCustomersReponse> Handle(GetCustomersRequest request, CancellationToken cancellationToken=default)
    {
        return await Task.FromResult(new GetCustomersReponse(new List<CustomerDto> {
            new (88, "customer 88"),
            new (99, "customer 99")
        }));
    }
}
