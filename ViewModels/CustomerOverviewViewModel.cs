using Blumen.Models;
using Blumen.Persistence;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Blumen.ViewModels
{
    public class CustomerOverviewViewModel : ObservableObject
    {
        #region Fields
        private CustomerRepo customerRepo = App.CustomerRepo;
        #endregion

        #region Constructors
        public ObservableCollection<Customer> Customers { get => customerRepo.GetItems(); }
        #endregion

        #region Properties

        #endregion

        #region Methods

        #endregion
    }
}
