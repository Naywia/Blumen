using Blumen.Models;
using System.Collections.ObjectModel;

namespace Blumen.ViewModels
{
    public class ProductTypeOverviewViewModel : ObservableObject
    {
        public ObservableCollection<ProductType> ProductTypes { get => App.ProductTypeRepo.GetItems(); }
    }
}
