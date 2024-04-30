using Blumen.ViewModels;
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
    /// Interaction logic for AddProductTypeView.xaml
    /// </summary>
    public partial class AddProductTypeView : Window
    {
        public AddProductTypeView()
        {
            InitializeComponent();
            DataContext = new AddProductTypeViewModel(this);
        }
        private void CloseAddProductType(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
