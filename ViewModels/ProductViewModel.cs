using Blumen.Models;
using Blumen.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32;

namespace Blumen.ViewModels
{
    public class ProductViewModel : ObservableObject
    {
        #region Fields
        private ICommand updateProductCommand;
        private ICommand deleteProductCommand;
        private ProductRepo productRepo = new();
        private ProductTypeRepo productTypeRepo = new();
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
                updateProductCommand ??= new RelayCommand(MethodToRun => UpdateProduct());
                return updateProductCommand;
            }
        }
        public ICommand DeleteProductCommand
        {
            get
            {
                deleteProductCommand ??= new RelayCommand(MethodToRun => DeleteProduct());
                return deleteProductCommand;
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
                return productTypeRepo.GetItems();
            }
        }
        #endregion

        #region Methods
        public void UpdateProduct()
        {
            bool updated = productRepo.UpdateItem(product, new Product()
            {
                ProductID = product.ProductID,
                Name = Name,
                Price = Price,
                Description = Description,
                Quantity = Quantity,
                Type = SelectedProductType
            });
            currentWindow.Close();
        }
        public void DeleteProduct()
        {
            if (MessageBox.Show("Er du sikker på du vil slette produktet", "Slet produkt", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                productRepo.RemoveItem(product);
                currentWindow.Close();
            }
        }
        #endregion
    }
}
