using Blumen.Models;
using Blumen.Persistence;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Blumen.ViewModels
{
    public class AddOrderViewModel : ObservableObject
    {
        #region Fields
        private ICommand addOrderCommand;
        private OrderRepo orderRepo = new();
        private ProductRepo productRepo = new();
        private CustomerRepo customerRepo = new();
        private Window currentWindow;

        private ObservableCollection<Product> allProducts;
        private ObservableCollection<Product> products;
        private string comment = "";
        private double price = 0.0;
        private DateTime orderDate = DateTime.Now;
        private string delivery = "";
        private Payment paymentStatus;
        private string card = "";
        private string paymentNote = "";
        private List<string> paymentOptions = new List<string>();
        private ObservableCollection<Customer> customers;
        private ObservableCollection<Customer> customer;
        #endregion

        #region Constructors
        public AddOrderViewModel(Window window)
        {
            currentWindow = window;
            orderDate = DateTime.Now;
            AllProducts = productRepo.GetItems();
            Products = [];
            Customers = customerRepo.GetItems();
            Customer = [];
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
                addOrderCommand ??= new RelayCommand(MethodToRun => AddOrder());
                return addOrderCommand;
            }
        }
        public ObservableCollection<Product> AllProducts
        {
            get => allProducts;
            set
            {
                allProducts = value;
            }
        }
        public ObservableCollection<Product> Products
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

        public ObservableCollection<Customer> Customers
        {
            get => customers;
            set
            {
                customers = value;
            }
        }
        public ObservableCollection<Customer> Customer
        {
            get => customer;
            set
            {
                customer = value;
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

            orderRepo.AddOrderToCustomer(orderRepo.GetItems().Last(), Customer[0].CustomerID);
            currentWindow.Close();
        }

        public void SetPaymentStatus(string status)
        {
            _ = Enum.TryParse(status.Replace(" ", "_"), out Payment payment);
            PaymentStatus = payment;
        }

        public void AddProduct(int productIndex)
        {
            Product product = AllProducts[productIndex];
            if (product != null)
            {
                Products.Add(product);
                AllProducts.Remove(product);
            }
        }

        public void RemoveProduct(int productIndex)
        {
            Product product = Products[productIndex];
            if (product != null)
            {
                Products.Remove(product);
                AllProducts.Add(product);
            }
        }

        public void ChooseCustomer(int customerIndex)
        {
            Customer custom = Customers[customerIndex];
            if (Customer.Count >= 1)
            {
                MessageBox.Show("Du har allerede valgt en kunde, fjern venligst kunden for at vælge en ny.", "Fejl", MessageBoxButton.OK);
                Customers.Remove(custom);
                Customers.Add(custom);
            }
            else
            {
                Customer.Add(custom);
                Customers.Remove(custom);
            }
        }

        public void RemoveCustomer(int customerIndex)
        {
            Customer custom = Customer[customerIndex];

            Customer.Remove(custom);
            Customers.Add(custom);
        }
        #endregion
    }
}
