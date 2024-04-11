using Blumen.Models;
using Blumen.Persistence;
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
        private ICommand selectOrderCommand;
        private OrderRepo orderRepo = App.OrderRepo;
        public ObservableCollection<Order> Orders { get; set; }
        public Order Order { get; set; }
        public ICommand SelectOrderCommand {
            get
            {
                if (selectOrderCommand == null)
                    selectOrderCommand = new RelayCommand(MethodToRun => SelectOrder());
                return selectOrderCommand;
            }
        }

        public void SelectOrder()
        {

        }
    }
}
