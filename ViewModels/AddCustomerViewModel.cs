using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Blumen.ViewModels
{
    internal class AddCustomerViewModel
    {
        private ICommand addCustomerCommand;
        private CustomerRepo customerRepo = App.CustomerRepo;
        public Customer NewCustomer { get; set; }
        public ICommand AddCustomerCommand
        {
            get
            {
                if (addCustomerCommand == null)
                    addCustomerCommand = new RelayCommand(MethodToRun => AddCustomer());
                return addCustomerCommand;
            }
        }
        public void AddCustomer()
        {

        }
    }
}
