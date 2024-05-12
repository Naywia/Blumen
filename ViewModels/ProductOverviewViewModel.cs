using Blumen.Models;
using Blumen.Persistence;
using System.Collections.ObjectModel;

namespace Blumen.ViewModels
{
    public class ProductOverviewViewModel : ObservableObject
    {



        #region Fields
        private ProductRepo productRepo = new();
        private ProductTypeRepo productTypeRepo = new();

        private string searchText = "";
        private int selectedMinPrice;
        private int selectedMaxPrice;
        private ObservableCollection<ProductType> selectedProductTypes = [];
        private ObservableCollection<ProductType> productTypes;
        private ObservableCollection<Product> products;
        #endregion

        #region Constructors
        public ProductOverviewViewModel()
        {
            ObservableCollection<Product> products = productRepo.GetItems();
            MaxPrice = SelectedMaxPrice = (int)Math.Ceiling(products.MaxBy(x => x.Price).Price);
            MinPrice = SelectedMinPrice = (int)Math.Floor(products.MinBy(x => x.Price).Price);
            ProductTypes = productTypeRepo.GetItems();
            Search();
        }
        #endregion

        #region Properties

        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                Search();
                NotifyPropertyChanged();
            }
        }

        public int SelectedMinPrice
        {
            get => selectedMinPrice;
            set
            {
                selectedMinPrice = value;
                Search();
                NotifyPropertyChanged();
            }
        }
        public int MinPrice { get; }

        public int SelectedMaxPrice
        {
            get => selectedMaxPrice;
            set
            {
                selectedMaxPrice = value;
                Search();
                NotifyPropertyChanged();
            }
        }

        public int MaxPrice { get; }
        public ObservableCollection<ProductType> SelectedProductTypes
        {
            get => selectedProductTypes;
            private set
            {
                selectedProductTypes = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<ProductType> ProductTypes
        {
            get => productTypes;
            private set
            {
                productTypes = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Product> Products
        {
            get => products;
            private set
            {
                products = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Methods
        private void Search()
        {
            if (SearchText == "" && selectedProductTypes.Count <= 0)
            {
                Products = productRepo.GetItems().Where(c =>
                c.Price.InRange(SelectedMinPrice, SelectedMaxPrice)
                ).ToObservableCollection();
            }
            else if (SearchText == "" && selectedProductTypes.Count > 0)
            {
                Products = productRepo.GetItems().Where(c =>
                c.Price.InRange(SelectedMinPrice, SelectedMaxPrice) &&
                selectedProductTypes.HasItem(c.Type)
                ).ToObservableCollection();
            }
            else if (SearchText != "" && selectedProductTypes.Count <= 0)
            {
                Products = productRepo.GetItems().Where(c =>
                c.Price.InRange(SelectedMinPrice, SelectedMaxPrice) &&
                c.Name.StartsWith(searchText, StringComparison.CurrentCultureIgnoreCase)
                ).ToObservableCollection();
            }
            else
            {
                Products = productRepo.GetItems().Where(c =>
                c.Price.InRange(SelectedMinPrice, SelectedMaxPrice) &&
                selectedProductTypes.HasItem(c.Type) &&
                c.Name.StartsWith(searchText, StringComparison.CurrentCultureIgnoreCase)
                ).ToObservableCollection();
            }
        }

        public void FilterAdded(int productTypeIndex)
        {
            ProductType productType = ProductTypes[productTypeIndex];
            if (productType != null)
            {
                selectedProductTypes.Add(productType);
                ProductTypes.Remove(productType);
            }
            Search();
        }

        public void FilterRemoved(int productTypeIndex)
        {
            ProductType productType = selectedProductTypes[productTypeIndex];
            if (productType != null)
            {
                selectedProductTypes.Remove(productType);
                ProductTypes.Add(productType);
            }
            Search();
        }
        #endregion
    }
}
