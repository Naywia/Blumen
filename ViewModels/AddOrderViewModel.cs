using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blumen.ViewModels
{
    public class AddOrderViewModel
    {
        private ICommand addOrderCommand;
        private OrderRepo orderRepo = App.OrderRepo;
        public Order NewOrder { get; set; }
        public ICommand AddOrderCommand {
            get
            {
                if (addOrderCommand == null)
                    addOrderCommand = new RelayCommand(MethodToRun => AddOrder());
                return addOrderCommand;
            }
        }
        public void AddOrder()
        {

        }
    }
}
