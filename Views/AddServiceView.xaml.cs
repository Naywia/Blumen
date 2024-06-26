﻿using Blumen.ViewModels;
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
    /// Interaction logic for AddServiceView.xaml
    /// </summary>
    public partial class AddServiceView : Window
    {
        public AddServiceView()
        {
            InitializeComponent();
            DataContext = new AddServiceViewModel(this);
        }
        private void CloseAddService(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
