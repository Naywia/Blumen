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
        private DateTime selectedDate = DateTime.Now;
        #endregion

        #region Constructors
        public OrderOverviewViewModel()
        {
            customers = customerRepo.GetItems();
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
                        foreach (Order order in customer.Orders)
                        {
                            orderSearch.Add(order);
                        }
                    }
                    Orders = orderSearch;
                }

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
