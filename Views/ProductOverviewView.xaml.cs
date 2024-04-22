using Blumen.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Blumen.Views
{
    /// <summary>
    /// Interaction logic for ProductOverviewView.xaml
    /// </summary>
    public partial class ProductOverviewView : Page
    {
        public ProductOverviewView()
        {
            InitializeComponent();
            DataContext = new ProductOverviewViewModel();
        }

        private void OpenAddProduct(object sender, RoutedEventArgs e)
        {
            AddProductView addProductView = new();
            addProductView.ShowDialog();
        }

        private void OpenEditProduct(object sender, SelectionChangedEventArgs e)
        {
            if (ProductListView.SelectedIndex >= 0)
            {
                ProductView productView = new(ProductListView.SelectedIndex);
                productView.ShowDialog();
            }
        }
    }
}
