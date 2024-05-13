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
        ProductOverviewViewModel productOverviewViewModel;
        public ProductOverviewView()
        {
            InitializeComponent();
            productOverviewViewModel = new ProductOverviewViewModel();
            DataContext = productOverviewViewModel;
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

        private void ProductTypeFilterRemoved(object sender, SelectionChangedEventArgs e)
        {
            if (ProductTypeFilter.SelectedIndex != -1)
            {
                productOverviewViewModel.FilterRemoved(ProductTypeFilter.SelectedIndex);
            }
        }

        private void ProductTypeFilterAdded(object sender, SelectionChangedEventArgs e)
        {
            if (ProductTypesComboBox.SelectedIndex != -1)
            {
                productOverviewViewModel.FilterAdded(ProductTypesComboBox.SelectedIndex);
            }
        }
    }
}