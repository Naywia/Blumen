using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Blumen.ViewModels
{
    public class CustomerOverviewViewModel
    {
        private ICommand selectCustomerCommand;
        private CustomerRepo customerRepo = App.CustomerRepo;
        public ObservableCollection<Customer> Customers { get; set; }
        public ICommand SelectCustomerCommand
        {
            get
            {
                if (selectCustomerCommand == null)
                    selectCustomerCommand = new RelayCommand(MethodToRun => SelectCustomer());
                return selectCustomerCommand;
            }
        }

        public void SelectCustomer()
        {

        }
    }
}
