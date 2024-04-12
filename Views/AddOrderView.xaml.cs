using Blumen.ViewModels;
using System.Windows;

namespace Blumen.Views
{
    /// <summary>
    /// Interaction logic for AddOrderView.xaml
    /// </summary>
    public partial class AddOrderView : Window
    {
        public AddOrderView()
        {
            InitializeComponent();
            DataContext = new AddOrderViewModel(this);
        }
        private void CloseAddOrder(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
