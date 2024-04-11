using Blumen.Models;
using Blumen.Persistence;
using System.Windows;
using System.Windows.Input;

namespace Blumen.ViewModels
{
    public class AddOrderViewModel : ObservableObject
    {
        #region Fields
        private ICommand addOrderCommand;
        private OrderRepo orderRepo = App.OrderRepo;
        private Window currentWindow;

        private int orderNumber;
        private DateTime orderDate;
        private string deliveryAddress;
        private Payment paymentStatus;
        private string comment;
        #endregion

        #region Constructors
        public AddOrderViewModel(Window window)
        {
            currentWindow = window;
        }
        #endregion

        #region Properties
        public ICommand AddOrderCommand
        {
            get
            {
                if (addOrderCommand == null)
                    addOrderCommand = new RelayCommand(MethodToRun => AddOrder());
                return addOrderCommand;
            }
        }

        public int OrderNumber
        {
            get => orderNumber;
            set
            {
                orderNumber = value;
                NotifyPropertyChanged();
            }
        }
        public DateTime OrderDate
        {
            get => orderDate;
            set
            {
                orderDate = value;
                NotifyPropertyChanged();
            }
        }
        public string DeliveryAddress
        {
            get => deliveryAddress;
            set
            {
                deliveryAddress = value;
                NotifyPropertyChanged();
            }
        }
        public Payment PaymentStatus
        {
            get => paymentStatus;
            set
            {
                paymentStatus = value;
                NotifyPropertyChanged();
            }
        }
        public string Comment
        {
            get => comment;
            set
            {
                comment = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Methods
        public void AddOrder()
        {
            orderRepo.AddItem(new Order()
            {
                OrderNumber = OrderNumber,
                OrderDate = OrderDate,
                DeliveryAddress = DeliveryAddress,
                PaymentStatus = PaymentStatus,
                Comment = Comment
            });
            currentWindow.Close();
        }
        #endregion
    }
}
