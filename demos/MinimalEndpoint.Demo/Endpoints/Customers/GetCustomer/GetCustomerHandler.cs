namespace MinimalEndpoint.Demo.Endpoints.Customers.GetCustomer;

public class GetCustomerHandler : ITransient
{
    public async Task<GetCustomerResponse> Handle(GetCustomerRequest request, CancellationToken cancellationToken=default)
    {
        return await Task.FromResult(new GetCustomerResponse(request.Id, "Customer's Name"));
    }
}
