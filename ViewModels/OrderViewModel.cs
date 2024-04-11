using Blumen.Models;
using Blumen.Persistence;

namespace Blumen.ViewModels
{
    public class OrderViewModel
    {
        private OrderRepo orderRepo = App.OrderRepo;
        public Order Order { get; set; }
    }
}
