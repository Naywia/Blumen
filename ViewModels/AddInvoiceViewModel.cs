﻿using Blumen.Models;
using Blumen.Persistence;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Blumen.ViewModels
{
    public class AddInvoiceViewModel : ObservableObject
    {
        #region Fields
        private InvoiceRepo invoiceRepo = new();
        private OrderRepo orderRepo = new();
        private CustomerRepo customerRepo = new();
        private Customer customer;
        private Window currentWindow;

        private ICommand addInvoiceCommand;
        private ObservableCollection<Order> invoiceOrders;
        private long invoiceNumber = 0;
        private string invoiceAddress = "";
        private DateTime invoiceDate = DateTime.Now;
        private string comment = "";
        #endregion

        #region Constructors
        public AddInvoiceViewModel(Window window, int customerIndex)
        {
            currentWindow = window;
            customer = customerRepo.GetItems()[customerIndex];
            invoiceOrders = orderRepo.GetOrdersNotInInvoice(customer.CustomerID);
            invoiceAddress = customer.Address;
            invoiceDate = DateTime.Now;
        }
        #endregion

        #region Properties
        public ICommand AddInvoiceCommand
        {
            get
            {
                addInvoiceCommand ??= new RelayCommand(MethodToRun => AddInvoice());
                return addInvoiceCommand;
            }
        }

        public string CustomerName
        {
            get => customer.Name;
        }

        public string CustomerAddress
        {
            get => customer.Address;
        }

        public string CustomerEmail
        {
            get => customer.Email;
        }

        public long CustomerPhoneNumber
        {
            get => customer.PhoneNumber;
        }

        public long InvoiceNumber
        {
            get => invoiceNumber;
            set
            {
                invoiceNumber = value;
                NotifyPropertyChanged();
            }
        }
        public string InvoiceAddress
        {
            get => invoiceAddress;
            set
            {
                invoiceAddress = value;
                NotifyPropertyChanged();
            }
        }
        public DateTime InvoiceDate
        {
            get => invoiceDate;
            set
            {
                invoiceDate = value;
                NotifyPropertyChanged();
            }
        }
        public string Comment
        {
            get => comment;
            set
            {
                comment = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<Order> InvoiceOrders
        {
            get => invoiceOrders;
            set
            {
                invoiceOrders = value;
                NotifyPropertyChanged();
            }
        }

        public double TotalPrice
        {
            get
            {
                double total = 0;
                foreach (Order order in invoiceOrders)
                {
                    total += order.Price;
                }
                return total;
            }
        }
        #endregion

        #region Methods
        public void AddInvoice()
        {
            invoiceRepo.AddItem(new Invoice()
            {
                InvoiceNumber = invoiceNumber,
                InvoiceAddress = invoiceAddress,
                InvoiceDate = invoiceDate,
                Comment = comment,
                InvoiceOrders = invoiceOrders
            });
            currentWindow.Close();
        }
        #endregion
    }
}
