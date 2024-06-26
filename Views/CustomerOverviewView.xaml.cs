﻿using Blumen.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Blumen.Views
{
    /// <summary>
    /// Interaction logic for CustomerOverviewView.xaml
    /// </summary>
    public partial class CustomerOverviewView : Page
    {
        public CustomerOverviewView()
        {
            InitializeComponent();
            DataContext = new CustomerOverviewViewModel();
        }

        private void OpenAddCustomer(object sender, RoutedEventArgs e)
        {
            AddCustomerView addCustomerView = new();
            addCustomerView.ShowDialog();
        }

        private void OpenEditCustomer(object sender, SelectionChangedEventArgs e)
        {
            if (CustomerListView.SelectedIndex >= 0)
            {
                CustomerView customerView = new(CustomerListView.SelectedIndex);
                customerView.ShowDialog();
            }
        }
    }
}
