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
        CustomerRepo repo = new();
        #endregion

        #region Constructors
        public ObservableCollection<Customer> Customers { get => repo.GetItems(); }
        #endregion

        #region Properties

        #endregion

        #region Methods

        #endregion
    }
}
