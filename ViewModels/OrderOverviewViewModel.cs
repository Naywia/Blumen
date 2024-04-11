using Blumen.Models;
using Blumen.Persistence;
using System.Collections.ObjectModel;

namespace Blumen.ViewModels
{
    public class OrderOverviewViewModel
    {
        #region Fields
        private OrderRepo orderRepo = App.OrderRepo;

        #endregion

        #region Constructors

        #endregion

        #region Properties
        public ObservableCollection<Order> Orders { get => orderRepo.GetItems(); }
        #endregion

        #region Methods

        #endregion
    }
}
