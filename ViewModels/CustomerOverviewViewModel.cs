using Blumen.Models;
using Blumen.Persistence;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Blumen.ViewModels
{
    public class CustomerOverviewViewModel : ObservableObject
    {
        #region Fields
        private CustomerRepo customerRepo = new();

        private string searchText = "";
        private ObservableCollection<Customer> customers;
        #endregion

        #region Constructors
        public CustomerOverviewViewModel()
        {
            if (SearchText == "")
            {
                Customers = customerRepo.GetItems();
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
                    Customers = customerRepo.GetItems();
                }
                else
                {
                    Customers = customerRepo.GetItems().Where(c => c.Name.StartsWith(searchText, StringComparison.CurrentCultureIgnoreCase)).ToObservableCollection();
                }
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<Customer> Customers
        {
            get => customers;
            private set
            {
                customers = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Methods

        #endregion
    }
}
