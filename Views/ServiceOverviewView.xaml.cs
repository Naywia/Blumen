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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Blumen.Views
{
    /// <summary>
    /// Interaction logic for ServiceOverviewView.xaml
    /// </summary>
    public partial class ServiceOverviewView : Page
    {
        public ServiceOverviewView()
        {
            InitializeComponent();
            DataContext = new ServiceOverviewViewModel();
        }

        private void OpenAddService(object sender, RoutedEventArgs e)
        {
            AddServiceView addServiceView = new AddServiceView();
            addServiceView.ShowDialog();
        }
        
        private void OpenEditService(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
