using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blumen.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public List<Product> Products { get; set; } //Skal tilføjes til OrderView.xaml
        public string Comment { get; set; }
        public double Price { get; set; }
        public DateTime OrderDate { get; set; }
        public string Delivery {  get; set; }
        public Payment PaymentStatus { get; set; }
        public string Card {  get; set; }
        public string PaymentNote { get; set; }
        public bool IsComplete { get; set; }

        public int GetTotal()
        {
            throw new NotImplementedException();
        }
    }
}
