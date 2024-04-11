using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blumen.ViewModels
{
    internal class CustomerViewModel
    {
        private ICommand updateCustomerCommand;
        private CustomerRepo customerRepo = App.CustomerRepo;
        public Customer Customer { get; set; }

        public ICommand UpdateCustomerCommand
        {
            get
            {
                if (updateCustomerCommand == null)
                    updateCustomerCommand = new RelayCommand(MethodToRun => UpdateCustomer());
                return updateCustomerCommand;
            }
        }

        public void UpdateCustomer()
        {

        }
    }
}
