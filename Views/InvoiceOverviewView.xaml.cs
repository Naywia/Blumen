using Blumen.ViewModels;
using System.Windows.Controls;

namespace Blumen.Views
{
    /// <summary>
    /// Interaction logic for InvoiceOverviewView.xaml
    /// </summary>
    public partial class InvoiceOverviewView : Page
    {
        public InvoiceOverviewView()
        {
            InitializeComponent();
            DataContext = new InvoiceOverviewViewModel();
        }

        private void OpenEditInvoice(object sender, SelectionChangedEventArgs e)
        {
            if (InvoiceListView.SelectedIndex >= 0)
            {
                InvoiceView invoiceView = new(InvoiceListView.SelectedIndex);
                invoiceView.ShowDialog();
            }
        }
    }
}
