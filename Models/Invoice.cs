using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blumen.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public long InvoiceNumber { get; set; }
        public string InvoiceAddress { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Comment { get; set; }
        public ObservableCollection<Order> InvoiceOrders { get; set; }
    }
}
