using Blumen.ViewModels;
using System.Windows;

namespace Blumen.Views
{
    /// <summary>
    /// Interaction logic for InvoiceView.xaml
    /// </summary>
    public partial class InvoiceView : Window
    {
        public InvoiceView(int invoiceIndex)
        {
            InitializeComponent();
            InvoiceViewModel invoiceViewModel = new InvoiceViewModel(this, invoiceIndex);
            DataContext = invoiceViewModel;
        }

        public void CloseInvoice(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
