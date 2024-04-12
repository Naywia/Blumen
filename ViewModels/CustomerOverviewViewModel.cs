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
        #endregion

        #region Constructors
        public ObservableCollection<Customer> Customers { get => App.CustomerRepo.GetItems(); }
        #endregion

        #region Properties

        #endregion

        #region Methods

        #endregion
    }
}
