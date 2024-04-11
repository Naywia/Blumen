using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blumen.ViewModels
{
    public class OrderOverviewViewModel
    {
        private OrderRepo orderRepo = App.OrderRepo;
        public ObservableCollection<Order> Orders { get; set; }
        public Order Order { get; set; }
        public ICommand SelectOrderCommand { get; set; }
    }
}
