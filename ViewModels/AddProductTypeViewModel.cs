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
    public class AddProductTypeViewModel : ObservableObject
    {
        #region Fields
        private ICommand addProductTypeCommand;
        private ProductTypeRepo productTypeRepo = new();
        private Window currentWindow;

        private string name;
        #endregion

        #region Constructors
        public AddProductTypeViewModel(Window window)
        {
            currentWindow = window;
        }
        #endregion

        #region Properties
        public ICommand AddProductTypeCommand
        {
            get
            {
                if (addProductTypeCommand == null)
                    addProductTypeCommand = new RelayCommand(MethodToRun => AddProductType());
                return addProductTypeCommand;
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
        #endregion

        #region Methods
        public void AddProductType()
        {
            productTypeRepo.AddItem(new ProductType()
            {
                Name = Name
            });
            currentWindow.Close();
        }
        #endregion
    }
}
