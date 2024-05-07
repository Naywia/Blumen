using Blumen.ViewModels;
using System.Windows;

namespace Blumen.Views
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : Window
    {
        private int customerIndex;
        public CustomerView(int itemIndex)
        {
            InitializeComponent();
            customerIndex = itemIndex;
            DataContext = new CustomerViewModel(this, itemIndex);
        }

        private void CloseUpdateCustomer(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GenerateInvoice(object sender, RoutedEventArgs e)
        {
            InvoiceView invoiceView = new(customerIndex);
            invoiceView.ShowDialog();
        }
    }
}
