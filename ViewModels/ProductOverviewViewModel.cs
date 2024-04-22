using Blumen.Models;
using System.Collections.ObjectModel;

namespace Blumen.ViewModels
{
    public class ProductOverviewViewModel : ObservableObject
    {
        public ObservableCollection<Product> Products { get => App.ProductRepo.GetItems(); }
    }
}
