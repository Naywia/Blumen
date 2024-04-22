using Blumen.ViewModels;
using System.Windows;
using System.Windows.Controls;

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
            AddOrderViewModel addOrderViewModel = new AddOrderViewModel(this);
            DataContext = addOrderViewModel;
            foreach (string name in addOrderViewModel.PaymentOptions)
            {
                var radiobutton = new RadioButton();
                radiobutton.Content = name.Replace("_", " "); ;
                radiobutton.GroupName = "PaymentOptions";
                PaymentOptions.Children.Add(radiobutton);
            }
        }
        private void CloseAddOrder(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
