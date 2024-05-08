using Blumen.Models;
using Blumen.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Blumen.ViewModels
{
    class AddServiceViewModel : ObservableObject
    {
        #region Fields
        private ServiceRepo serviceRepo = new ServiceRepo();
        private Window currentWindow;
        private ICommand addServiceCommand;
        private string name;
        private double price;
        #endregion

        #region Constructors
        public AddServiceViewModel(Window window)
        {
            currentWindow = window;
        }
        #endregion

        #region Properties
        public ICommand AddServiceCommand
        {
            get
            {
                addServiceCommand ??= new RelayCommand(MethodToRun => AddService());
                return addServiceCommand;
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
        #endregion

        #region Methods
        public void AddService()
        {
            serviceRepo.AddItem(new Service()
            {
                Name = Name,
                Price = Price,
            });
            currentWindow.Close();
        }
        #endregion
    }
}
