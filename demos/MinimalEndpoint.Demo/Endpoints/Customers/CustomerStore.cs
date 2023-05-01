namespace MinimalEndpoint.Demo.Endpoints.Customers
{
    public class CustomerStore : ITransient, ICustomerStore
    {
        public void Add(string name)
        {
            Console.WriteLine($"adding customer : {name}");
        }
    }
}
