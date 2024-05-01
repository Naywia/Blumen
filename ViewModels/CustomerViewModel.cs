using Blumen.Models;
using Blumen.Persistence;
using System.Windows;
using System.Windows.Input;

namespace Blumen.ViewModels
{
    public class CustomerViewModel : ObservableObject
    {
        #region Fields
        private ICommand updateCustomerCommand;
        private CustomerRepo customerRepo = new();
        private Customer customer;
        private Window currentWindow;

        private string name;
        private string address;
        private long phoneNumber;
        private string email;
        private long paymentNumber;
        private PaymentNumberType selectedPaymentNumberType;
        #endregion

        #region Constructors
        public CustomerViewModel(Window window, int customerIndex)
        {
            currentWindow = window;
            customer = customerRepo.GetItems()[customerIndex];

            Name = customer.Name;
            Address = customer.Address;
            PhoneNumber = customer.PhoneNumber;
            Email = customer.Email;
            PaymentNumber = customer.PaymentNumber;
            SelectedPaymentNumberType = customer.PaymentNumberType;
        }

        #endregion

        #region Properties
        public ICommand UpdateCustomerCommand
        {
            get
            {
                if (updateCustomerCommand == null)
                    updateCustomerCommand = new RelayCommand(MethodToRun => UpdateCustomer());
                return updateCustomerCommand;
            }
        }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }
        public string Address
        {
            get => address;
            set
            {
                address = value;
                NotifyPropertyChanged();
            }
        }
        public long PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                NotifyPropertyChanged();
            }
        }
        public string Email
        {
            get => email;
            set
            {
                email = value;
                NotifyPropertyChanged();
            }
        }
        public long PaymentNumber
        {
            get => paymentNumber;
            set
            {
                paymentNumber = value;
                NotifyPropertyChanged();
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

        public IEnumerable<PaymentNumberType> PaymentNumberType
        {
            get
            {
                return Enum.GetValues(typeof(PaymentNumberType)).Cast<PaymentNumberType>();
            }
        }
        #endregion

        #region Methods
        public void UpdateCustomer()
        {
            customerRepo.UpdateItem(customer, new Customer()
            {
                Name = Name,
                Address = Address,
                PhoneNumber = PhoneNumber,
                Email = Email,
                PaymentNumber = PaymentNumber,
                PaymentNumberType = SelectedPaymentNumberType
            });
            currentWindow.Close();
        }
        #endregion
    }
}
