using Blumen.Models;
using Blumen.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace Blumen.ViewModels
{
    internal class InvoiceViewModel : ObservableObject
    {
        #region Fields
        private InvoiceRepo invoiceRepo = new();
        private OrderRepo orderRepo = new();
        private CustomerRepo customerRepo = new();
        private Invoice invoice;
        private Customer customer;
        private Window currentWindow;
        #endregion

        #region Constructors
        public InvoiceViewModel(Window window, int invoiceIndex)
        {
            currentWindow = window;
            invoice = invoiceRepo.GetItems()[invoiceIndex];
            customer = customerRepo.GetCustomerFromItem(invoice.InvoiceOrders[0]);
        }
        #endregion

        #region Properties

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
            get => invoice.InvoiceNumber;
        }

        public string InvoiceAddress
        {
            get => invoice.InvoiceAddress;
        }

        public DateTime InvoiceDate
        {
            get => invoice.InvoiceDate;
        }

        public string Comment
        {
            get => invoice.Comment;
        }

        public ObservableCollection<Order> InvoiceOrders
        {
            get => invoice.InvoiceOrders;
        }

        public double TotalPrice
        {
            get
            {
                double total = 0;
                foreach (Order order in invoice.InvoiceOrders)
                {
                    total += order.Price;
                }
                return total;
            }
        }
        #endregion

        #region Methods

        #endregion
    }
}
