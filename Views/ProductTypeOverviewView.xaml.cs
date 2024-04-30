using Blumen.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Blumen.Views
{
    /// <summary>
    /// Interaction logic for ProductTypeOverviewView.xaml
    /// </summary>
    public partial class ProductTypeOverviewView : Page
    {
        public ProductTypeOverviewView()
        {
            InitializeComponent();
            DataContext = new ProductTypeOverviewViewModel();
        }

        private void OpenAddProductType(object sender, RoutedEventArgs e)
        {
            AddProductTypeView addProductTypeView = new();
            addProductTypeView.ShowDialog();
        }

        private void OpenEditProductType(object sender, SelectionChangedEventArgs e)
        {
            //if (ProductTypeListView.SelectedIndex >= 0)
            //{
            //    ProductTypeView productTypeView = new(ProductTypeListView.SelectedIndex);
            //    productTypeView.ShowDialog();
            //}
        }
    }
}
