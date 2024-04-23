using Blumen.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Blumen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void NavigateToFrontpage(object sender, RoutedEventArgs e)
        //{
        //    ContentFrame.Navigate(new AddCustomerView());
        //}

        private void NavigateToOrderOverview(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new OrderOverviewView());
        }

        private void NavigateToProductOverview(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ProductOverviewView());
        }

        private void NavigateToProductTypeOverview(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ProductTypeOverviewView());
        }

        private void NavigateToCustomerOverview(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new CustomerOverviewView());
        }
    }
}