namespace MinimalEndpoint.Demo.Endpoints.Customers.CreateCustomer;

public class CreateCustomerHandler: CustomerServiceBase, ITransient
{
    public async Task<CreateCustomerResponse> Handle(CreateCustomerRequest request, CancellationToken cancellationToken = default)
    {

        CustomerStore.Add(request.Name);
        return await Task.FromResult(new CreateCustomerResponse(99));
    }
}
