using Blumen.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Blumen.Views
{
    /// <summary>
    /// Interaction logic for OrderOverviewView.xaml
    /// </summary>
    public partial class OrderOverviewView : Page
    {
        public OrderOverviewView()
        {
            InitializeComponent();
            DataContext = new OrderOverviewViewModel();
        }
        private void OpenAddOrder(object sender, RoutedEventArgs e)
        {
            AddOrderView addOrderView = new();
            addOrderView.ShowDialog();
        }

        private void OpenEditOrder(object sender, SelectionChangedEventArgs e)
        {
            if (OrderListView.SelectedIndex >= 0)
            {
                OrderView orderView = new(OrderListView.SelectedIndex);
                orderView.ShowDialog();
            }
        }
    }
}
