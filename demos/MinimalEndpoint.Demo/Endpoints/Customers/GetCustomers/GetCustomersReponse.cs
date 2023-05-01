namespace MinimalEndpoint.Demo.Endpoints.Customers.GetCustomers;

public record GetCustomersReponse(List<CustomerDto> Result);


public record CustomerDto(int Id, string Name);
