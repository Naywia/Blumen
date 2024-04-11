using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blumen.Models
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string DeliveryAddress { get; set; }
        public Payment PaymentStatus { get; set; }
        public string Comment { get; set; }

        public int GetTotal()
        {
            throw new NotImplementedException();
        }
    }
}
