using Blumen.Models;
using Blumen.Persistence;
using System.Collections.ObjectModel;

namespace Blumen.ViewModels
{
    public class OrderOverviewViewModel : ObservableObject
    {
        #region Fields
        private OrderRepo orderRepo = new();
        #endregion

        #region Constructors
        public OrderOverviewViewModel() {
        }
        #endregion

        #region Properties
        public ObservableCollection<Order> Orders { get => orderRepo.GetItems(); }
        #endregion

        #region Methods

        #endregion
    }
}
