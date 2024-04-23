using Blumen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blumen.Persistence
{
    public class ProductTypeRepo : Repo<ProductType>
    {
        public ProductType GetProductType(string name)
        {
            return repo.Where(p => name == p.Name).FirstOrDefault()!;
        }
    }
}
