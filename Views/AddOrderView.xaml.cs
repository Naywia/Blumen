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
        AddOrderViewModel addOrderViewModel;
        public AddOrderView()
        {
            InitializeComponent();
            addOrderViewModel = new AddOrderViewModel(this);
            DataContext = addOrderViewModel;
            foreach (string name in addOrderViewModel.PaymentOptions)
            {
                var radiobutton = new RadioButton();
                radiobutton.Content = name.Replace("_", " ");
                radiobutton.Checked += RadioButton_Checked;
                radiobutton.GroupName = "PaymentOptions";
                PaymentOptions.Children.Add(radiobutton);
            }

            
        }
        private void CloseAddOrder(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var checkedValue = PaymentOptions.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked.HasValue && r.IsChecked.Value);
            addOrderViewModel.SetPaymentStatus(checkedValue.Content.ToString()!);
        }
    }
}
