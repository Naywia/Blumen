using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blumen.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
        public long PaymentNumber { get; set; }
        public PaymentNumberType PaymentNumberType { get; set; }
        public ObservableCollection<Order> Orders { get; set; }
    }
}
