using Blumen.ViewModels;
using System.Windows;

namespace Blumen.Views
{
    /// <summary>
    /// Interaction logic for AddProductView.xaml
    /// </summary>
    public partial class AddProductView : Window
    {
        public AddProductView()
        {
            InitializeComponent();
            DataContext = new AddProductViewModel(this);
        }

        private void CloseAddProduct(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
