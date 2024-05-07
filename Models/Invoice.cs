using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blumen.Models
{
    public class Invoice
    {
        public long InvoiceNumber { get; set; }
        public string InvoiceAddress { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Comment { get; set; }
    }
}
