using Blumen.ViewModels;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        public void GenerateInvoice(object sender, RoutedEventArgs e)
        {

        }
    }
}
