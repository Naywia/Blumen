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
    public class AddProductViewModel : ObservableObject
    {
        #region Fields
        private ICommand addProductCommand;
        private ProductRepo productRepo = App.ProductRepo;
        private Window currentWindow;

        private string name;
        private double price;
        private string description;
        private int quantity;
        private ProductType type;
        #endregion

        #region Constructors
        public AddProductViewModel(Window window)
        {
            currentWindow = window;
        }
        #endregion

        #region Properties
        public ICommand AddProductCommand
        {
            get
            {
                if (addProductCommand == null)
                    addProductCommand = new RelayCommand(MethodToRun => AddProduct());
                return addProductCommand;
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
        public ProductType Type
        {
            get => type;
            set
            {
                type = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Methods
        public void AddProduct()
        {
            productRepo.AddItem(new Product()
            {
                Name = Name,
                Price = Price,
                Description = Description,
                Quantity = Quantity,
                Type = Type
            });
            currentWindow.Close();
        }
        #endregion
    }
}
