using Blumen.Models;
using Blumen.Persistence;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace Blumen.ViewModels
{
    internal class AddCustomerViewModel : ObservableObject
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
            customerRepo.AddItem(new Customer() { Name = this.Name, Address = this.Address, PhoneNumber = this.PhoneNumber, Email = this.Email, PaymentNumber = this.PaymentNumber });
            currentWindow.Close();
        }
        private Window currentWindow;
        public AddCustomerViewModel(Window window) 
        {
            currentWindow = window;
        }

        public IEnumerable<PaymentNumberType> PaymentNumberType
        {
            get
            {
                return Enum.GetValues(typeof(PaymentNumberType)).Cast<PaymentNumberType>();
            }
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
