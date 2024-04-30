using Blumen.Models;

namespace Blumen.Persistence
{
    public class CustomerRepo : Repo<Customer>
    {
        public Customer GetCustomer(string name)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomer(int phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
