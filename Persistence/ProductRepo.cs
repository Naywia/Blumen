using Blumen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Blumen.Persistence
{
    public class ProductRepo : Repo<Product>
    {
        public Product GetProduct(string name)
        {
            return repo.Where(p => name == p.Name).FirstOrDefault()!;
        }
    }
}
