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
            Orders.Add(new Order { OrderDate = DateTime.Now, Delivery = "hejvej, Hentes i morgen", PaymentStatus = Payment.MobilePay, Card = "Dejlige dig", PaymentNote = "", Price = 200.22, Comment = "Rød, blå" });
        }
        #endregion

        #region Properties
        public ObservableCollection<Order> Orders { get => App.OrderRepo.GetItems(); }
        #endregion

        #region Methods

        #endregion
    }
}
