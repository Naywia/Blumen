using Blumen.Models;
using Blumen.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Blumen.ViewModels
{
    public class ProductViewModel : ObservableObject
    {
        #region Fields
        private ICommand updateProductCommand;
        private ProductRepo productRepo = App.ProductRepo;
        private Product product;
        private Window currentWindow;

        private string name;
        private double price;
        private string description;
        private int quantity;
        private ProductType selectedType;
        #endregion

        #region Constructors
        public ProductViewModel(Window window, int productIndex)
        {
            currentWindow = window;
            product = productRepo.GetItems()[productIndex];

            Name = product.Name;
            Price = product.Price;
            Description = product.Description;
            Quantity = product.Quantity;
            SelectedProductType = product.Type;
        }
        #endregion

        #region Properties
        public ICommand UpdateProductCommand
        {
            get
            {
                if (updateProductCommand == null)
                    updateProductCommand = new RelayCommand(MethodToRun => UpdateProduct());
                return updateProductCommand;
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }

        public double Price
        {
            get => price;
            set
            {
                price = value;
                NotifyPropertyChanged();
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                NotifyPropertyChanged();
            }
        }

        public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                NotifyPropertyChanged();
            }
        }
        public ProductType SelectedProductType
        {
            get => selectedType;
            set
            {
                selectedType = value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<ProductType> ProductTypes
        {
            get
            {
                return Enum.GetValues(typeof(ProductType)).Cast<ProductType>();
            }
        }
        #endregion

        #region Methods
        public void UpdateProduct()
        {
            productRepo.UpdateItem(product, new Product()
            {
                Name = Name,
                Price = Price,
                Description = Description,
                Quantity = Quantity,
                Type = SelectedProductType
        });
            currentWindow.Close();
        }
    #endregion
}
}
