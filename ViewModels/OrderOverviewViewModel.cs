using Blumen.Models;
using Blumen.Persistence;
using System.Collections.ObjectModel;

namespace Blumen.ViewModels
{
    public class OrderOverviewViewModel : ObservableObject
    {
        #region Fields

        #endregion

        #region Constructors

        #endregion

        #region Properties
        public ObservableCollection<Order> Orders { get => App.OrderRepo.GetItems(); }
        #endregion

        #region Methods

        #endregion
    }
}
