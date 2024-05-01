using Blumen.Models;
using Blumen.Persistence;
using System.Collections.ObjectModel;

namespace Blumen.ViewModels
{
    public class ProductTypeOverviewViewModel : ObservableObject
    {
        private ProductTypeRepo productTypeRepo = new();
        public ObservableCollection<ProductType> ProductTypes { get => productTypeRepo.GetItems(); }
    }
}
