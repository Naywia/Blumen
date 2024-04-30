using Blumen.Models;
using System.Collections.ObjectModel;

namespace Blumen.ViewModels
{
    public class OrderOverviewViewModel : ObservableObject
    {
        #region Fields

        #endregion

        #region Constructors
        public OrderOverviewViewModel() {
        }
        #endregion

        #region Properties
        public ObservableCollection<Order> Orders { get => App.OrderRepo.GetItems(); }
        #endregion

        #region Methods

        #endregion
    }
}
