using Blumen.ViewModels;
using System.Windows;

namespace Blumen.Views
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : Window
    {
        public CustomerView(int itemIndex)
        {
            InitializeComponent();
            DataContext = new CustomerViewModel(this, itemIndex);
        }

        private void CloseUpdateCustomer(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
