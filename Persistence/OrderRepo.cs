using Blumen.Models;

namespace Blumen.Persistence
{
    public class OrderRepo : Repo<Order>
    {
        public Order GetOrder(int orderNumber)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(DateTime orderDate)
        {
            throw new NotImplementedException();
        }
    }
}
