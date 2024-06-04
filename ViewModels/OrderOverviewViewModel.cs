using Blumen.Models;
using Blumen.Persistence;
using System.Collections.ObjectModel;

namespace Blumen.ViewModels
{
    public class OrderOverviewViewModel : ObservableObject
    {
        #region Fields
        private OrderRepo orderRepo = new();
        private CustomerRepo customerRepo = new();

        private ObservableCollection<Customer> customers;

        private string searchText = "";
        private ObservableCollection<Order> orders;
        private ObservableCollection<Order> allOrders;
        private DateTime selectedDate = DateTime.Now;
        #endregion

        #region Constructors
        public OrderOverviewViewModel()
        {
            customers = customerRepo.GetItems();
            allOrders = orderRepo.GetItems();
            if (SearchText == "")
            {
                Orders = orderRepo.GetItems();
            }
        }
        #endregion

        #region Properties
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;

                Search();

                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<Order> Orders
        {
            get => orders;
            private set
            {
                orders = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Methods
        private void Search()
        {
            if (SearchText == "")
            {
                Orders = orderRepo.GetItems();
            }
            else
            {
                ObservableCollection<Order> orderSearch = [];
                List<Customer> customerSearch = customers.Where(c => c.Name.StartsWith(searchText, StringComparison.CurrentCultureIgnoreCase)).ToList();
                foreach (Customer customer in customerSearch)
                {
                    ObservableCollection<Order> os = allOrders.Where(o => o.Customer.CustomerID == customer.CustomerID).ToObservableCollection();
                    foreach (Order o in os)
                    {
                        orderSearch.Add(o);
                    }
                }
                Orders = orderSearch;
            }
        }

        public void UpdateOrderList()
        {
            Search();
        }
        //public string GetCustomer<T>(T item)
        //{
        //    switch (item)
        //    {
        //        case Order order:
        //            Customer customer = customers.Where(c => c.Orders.HasItem(order)).First();
        //            return customer.Name;
        //        default:
        //            return string.Empty;
        //    }
        //}
        #endregion
    }
}
