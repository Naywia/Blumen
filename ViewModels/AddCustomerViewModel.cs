using Blumen.Models;
using Blumen.Persistence;
using System.Windows;
using System.Windows.Input;

namespace Blumen.ViewModels
{
    public class AddCustomerViewModel : ObservableObject
    {
        #region Fields
        private ICommand addCustomerCommand;
        private CustomerRepo customerRepo = App.CustomerRepo;
        private Window currentWindow;

        private string name;
        private string address;
        private long phoneNumber;
        private string email;
        private long paymentNumber;
        private PaymentNumberType selectedPaymentNumberType;
        #endregion

        #region Constructors
        public AddCustomerViewModel(Window window)
        {
            currentWindow = window;
        }
        #endregion

        #region Properties
        public ICommand AddCustomerCommand
        {
            get
            {
                if (addCustomerCommand == null)
                    addCustomerCommand = new RelayCommand(MethodToRun => AddCustomer());
                return addCustomerCommand;
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
        public void AddCustomer()
        {
            customerRepo.AddItem(new Customer() { Name = this.Name, Address = this.Address, PhoneNumber = this.PhoneNumber, Email = this.Email, PaymentNumber = this.PaymentNumber });
            currentWindow.Close();
        }
        #endregion
    }
}
