﻿using Blumen.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Blumen.Views
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : Window
    {
        public OrderView(int orderIndex)
        {
            InitializeComponent();
            OrderViewModel orderViewModel = new OrderViewModel(this, orderIndex);
            DataContext = orderViewModel;
            foreach(string name in orderViewModel.PaymentOptions)
            {
                var radiobutton = new RadioButton();
                radiobutton.Content = name.Replace("_", " "); ;
                radiobutton.GroupName = "PaymentOptions";
                PaymentOptions.Children.Add(radiobutton);
            }
        }

        private void CloseUpdateOrder(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
