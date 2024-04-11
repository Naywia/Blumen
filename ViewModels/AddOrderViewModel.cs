using Blumen.Models;
using Blumen.Persistence;
using System.Windows;
using System.Windows.Input;

namespace Blumen.ViewModels
{
    public class AddOrderViewModel
    {
        #region Fields
        private ICommand addOrderCommand;
        private OrderRepo orderRepo = App.OrderRepo;
        private Window currentWindow;
        #endregion

        #region Constructors
        public AddOrderViewModel(Window window)
        {
            currentWindow = window;
        }
        #endregion

        #region Properties
        public ICommand AddOrderCommand
        {
            get
            {
                if (addOrderCommand == null)
                    addOrderCommand = new RelayCommand(MethodToRun => AddOrder());
                return addOrderCommand;
            }
        }
        #endregion

        #region Methods
        public void AddOrder()
        {

        }
        #endregion
    }
}
