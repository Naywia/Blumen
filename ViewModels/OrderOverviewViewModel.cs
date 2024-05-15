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

        private string searchText = "";
        private ObservableCollection<Order> orders;
        private DateTime selectedDate = DateTime.Now;
        #endregion

        #region Constructors
        public OrderOverviewViewModel()
        {
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
                    List<Customer> customerSearch = customerRepo.GetItems().Where(c => c.Name.StartsWith(searchText, StringComparison.CurrentCultureIgnoreCase)).ToList();
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

        #endregion
    }
}
