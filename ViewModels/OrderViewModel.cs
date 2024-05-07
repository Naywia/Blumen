using Blumen.Models;
using Blumen.Persistence;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Blumen.ViewModels
{
    public class OrderViewModel : ObservableObject
    {
        #region Fields
        private ICommand updateOrderCommand;
        private OrderRepo orderRepo = new();
        private Order order;
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
        public OrderViewModel(Window window, int orderIndex)
        {
            currentWindow = window;
            order = orderRepo.GetItems()[orderIndex];

            Products = order.Products;
            Comment = order.Comment;
            Price = order.Price;
            orderDate = order.OrderDate;
            Delivery = order.Delivery;
            PaymentStatus = order.PaymentStatus;
            Card = order.Card;
            PaymentNote = order.PaymentNote;

            foreach (string name in Enum.GetNames(typeof(Payment)))
            {
                PaymentOptions.Add(name);
            }
        }
        #endregion

        #region Properties
        public ICommand UpdateOrderCommand
        {
            get
            {
                updateOrderCommand ??= new RelayCommand(MethodToRun => UpdateOrder());
                return updateOrderCommand;
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
        public void UpdateOrder()
        {
            orderRepo.UpdateItem(order, new Order()
            {
                OrderID = order.OrderID,
                Products = order.Products,
                Comment = comment,
                Price = price,
                OrderDate = order.OrderDate,
                Delivery = delivery,
                PaymentStatus = order.PaymentStatus,
                Card = card,
                PaymentNote = paymentNote,
            });
            currentWindow.Close();
        }
        #endregion
    }
}
