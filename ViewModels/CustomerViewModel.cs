using Blumen.Models;
using Blumen.Persistence;
using System.Windows;
using System.Windows.Input;

namespace Blumen.ViewModels
{
    public class CustomerViewModel : ObservableObject
    {
        private ICommand updateCustomerCommand;
        private CustomerRepo customerRepo = App.CustomerRepo;
        private Customer customer;

        public ICommand UpdateCustomerCommand
        {
            get
            {
                if (updateCustomerCommand == null)
                    updateCustomerCommand = new RelayCommand(MethodToRun => UpdateCustomer());
                return updateCustomerCommand;
            }
        }

        private Window currentWindow;
        public CustomerViewModel(Window window, int customerIndex)
        {
            currentWindow = window;
            this.customer = customerRepo.GetItems()[customerIndex];

            Name = customer.Name;
            Address = customer.Address;
            PhoneNumber = customer.PhoneNumber;
            Email = customer.Email;
            PaymentNumber = customer.PaymentNumber;
        }

        public void UpdateCustomer()
        {
            customerRepo.UpdateItem(customer, new Customer() { Name = this.Name, Address = this.Address, PhoneNumber = this.PhoneNumber, Email = this.Email, PaymentNumber = this.PaymentNumber });
            currentWindow.Close();
        }

        public PaymentNumberType SelectedPaymentNumberType
        {
            get => selectedPaymentNumberType;
            set
            {
                selectedPaymentNumberType = value;
                NotifyPropertyChanged();
            }
        }
        private PaymentNumberType selectedPaymentNumberType;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }
        private string name;
        public string Address
        {
            get => address;
            set
            {
                address = value;
                NotifyPropertyChanged();
            }
        }
        private string address;
        public long PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                NotifyPropertyChanged();
            }
        }
        private long phoneNumber;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                NotifyPropertyChanged();
            }
        }
        private string email;
        public long PaymentNumber
        {
            get => paymentNumber;
            set
            {
                paymentNumber = value;
                NotifyPropertyChanged();
            }
        }
        private long paymentNumber;
    }
}
