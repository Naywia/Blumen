using Blumen.Models;
using Blumen.Persistence;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Blumen.ViewModels
{
    public class CustomerOverviewViewModel : ObservableObject
    {
        private ICommand selectCustomerCommand;
        private CustomerRepo customerRepo = App.CustomerRepo;
        public ObservableCollection<Customer> Customers { get => customerRepo.GetItems(); }
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

        public Customer SelectedCustomer
        {
            get => selectedCustomer;
            set
            {
                selectedCustomer = value;
                NotifyPropertyChanged();
            }
        }
        private Customer selectedCustomer;
    }
}
