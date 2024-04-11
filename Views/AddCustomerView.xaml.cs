using Blumen.ViewModels;
using System.Windows;

namespace Blumen.Views
{
    /// <summary>
    /// Interaction logic for AddCustomerView.xaml
    /// </summary>
    public partial class AddCustomerView : Window
    {
        public AddCustomerView()
        {
            InitializeComponent();
            DataContext = new AddCustomerViewModel(this);
        }

        private void CloseAddCustomer(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
