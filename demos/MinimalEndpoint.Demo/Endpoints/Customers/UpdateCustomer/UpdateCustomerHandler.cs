namespace MinimalEndpoint.Demo.Endpoints.Customers.UpdateCustomer;

public class UpdateCustomerHandler : ITransient
{
    public async Task<UpdateCustomerResponse> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken = default)
    {
        Console.WriteLine(request);
        return await Task.FromResult(new UpdateCustomerResponse());
    }
}
