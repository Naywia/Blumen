using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Blumen.Models
{
    public class ProductType
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}