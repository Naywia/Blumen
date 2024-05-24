using Blumen.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Blumen.Views
{
    /// <summary>
    /// Interaction logic for OrderOverviewView.xaml
    /// </summary>
    public partial class OrderOverviewView : Page
    {
        private OrderOverviewViewModel orderOverviewViewModel;
        public OrderOverviewView()
        {
            InitializeComponent();
            orderOverviewViewModel = new OrderOverviewViewModel();
            DataContext = orderOverviewViewModel;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //foreach (var item in OrderListView.ItemsSource)
            //{
            //    foreach (var control in FindVisualChildren<Label>(OrderListView))
            //    {
            //        if (control.Name == "CustomerLabel")
            //        {
            //            control.Content = orderOverviewViewModel.GetCustomer(item);
            //            break;
            //        }
            //    }
            //}
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
                //OrderListView.SelectedIndex = -1;
            }
        }


        public IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);

                    if (child != null && child is T)
                        yield return (T)child;

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                        yield return childOfChild;
                }
            }
        }
    }
}
