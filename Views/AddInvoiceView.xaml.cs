using Blumen.ViewModels;
using System.Windows;

namespace Blumen.Views
{
    /// <summary>
    /// Interaction logic for InvoiceView.xaml
    /// </summary>
    public partial class AddInvoiceView : Window
    {
        public AddInvoiceView(int customerIndex)
        {
            InitializeComponent();
            DataContext = new AddInvoiceViewModel(this, customerIndex);
        }

        private void CloseAddInvoice(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
