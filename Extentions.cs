using Blumen.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blumen
{
    public static class Extentions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }

        public static bool InRange(this double price, int min, int max)
        {
            return price >= min && price <= max;
        }

        public static bool HasItem(this ObservableCollection<ProductType> collection, ProductType item)
        {
            foreach (ProductType cItem in collection)
            {
                if (cItem.ProductTypeID == item.ProductTypeID)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
