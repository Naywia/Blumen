﻿using Blumen.Models;
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
        private ProductRepo productRepo = new();
        private ProductTypeRepo productTypeRepo = new();
        private Window currentWindow;

        private string name;
        private double price;
        private string description;
        private int quantity;
        private ProductType selectedType;
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
                addProductCommand ??= new RelayCommand(MethodToRun => AddProduct());
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
        public void AddProduct()
        {
            productRepo.AddItem(new Product()
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
