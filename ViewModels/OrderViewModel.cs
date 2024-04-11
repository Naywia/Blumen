using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blumen.ViewModels
{
    public class OrderViewModel
    {
        private OrderRepo orderRepo = App.OrderRepo;
        public Order Order { get; set; }
    }
}
