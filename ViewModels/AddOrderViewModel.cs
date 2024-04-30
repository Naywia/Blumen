using Blumen.Models;
using Blumen.Persistence;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Blumen.ViewModels
{
    public class AddOrderViewModel : ObservableObject
    {
        #region Fields
        private ICommand addOrderCommand;
        private OrderRepo orderRepo = App.OrderRepo;
        private Window currentWindow;

        private List<Product> products;
        private string comment;
        private double price;
        private DateTime orderDate;
        private string delivery;
        private Payment paymentStatus;
        private string card;
        private string paymentNote;
        private List<string> paymentOptions = new List<string>();
        #endregion

        #region Constructors
        public AddOrderViewModel(Window window)
        {
            currentWindow = window;
            orderDate = DateTime.Now;
            foreach (string name in Enum.GetNames(typeof(Payment)))
            {
                PaymentOptions.Add(name);
            }
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

        public List<Product> Products
        {
            get => products;
            set
            {
                products = value;
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
        public double Price
        {
            get => price;
            set
            {
                price = value;
                NotifyPropertyChanged();
            }
        }
        public TimeOnly OrderDateTime
        {
            get => TimeOnly.Parse($"{orderDate.Hour}:{orderDate.Minute}");
        }
        public string OrderDateDay
        {
            get => orderDate.DayOfWeek.ToString();
        }
        public DateOnly OrderDateDate
        {
            get => DateOnly.Parse($"{orderDate.Day}-{orderDate.Month}-{orderDate.Year}");
        }


        public string Delivery
        {
            get => delivery;
            set
            {
                delivery = value;
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
        public string Card
        {
            get => card;
            set
            {
                card = value;
                NotifyPropertyChanged();
            }
        }
        public string PaymentNote
        {
            get => paymentNote;
            set
            {
                paymentNote = value;
                NotifyPropertyChanged();
            }
        }
        public List<string> PaymentOptions
        {
            get => paymentOptions;
            private set
            {
                paymentOptions = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Methods
        public void AddOrder()
        {
            orderRepo.AddItem(new Order()
            {
                Products = products,
                Comment = comment,
                Price = price,
                OrderDate = orderDate,
                Delivery = delivery,
                PaymentStatus = paymentStatus,
                Card = card,
                PaymentNote = paymentNote,
            });
            currentWindow.Close();
        }

        public void SetPaymentStatus(string status)
        {
            Enum.TryParse(status.Replace(" ", "_"), out Payment payment);
            PaymentStatus = payment;
        }
        #endregion
    }
}
