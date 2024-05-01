using Blumen.Models;
using Blumen.Persistence;
using System.Collections.ObjectModel;

namespace Blumen.ViewModels
{
    public class ProductOverviewViewModel : ObservableObject
    {
        private ProductRepo productRepo = new();
        public ObservableCollection<Product> Products { get => productRepo.GetItems(); }
    }
}
