using Blumen.Models;
using Blumen.Persistence;
using System.Collections.ObjectModel;

namespace Blumen.ViewModels
{
    class ServiceOverviewViewModel : ObservableObject
    {
        #region Fields
        private ServiceRepo serviceRepo = new();

        private string searchText = "";
        private ObservableCollection<Service> services;
        #endregion

        #region Constructors
        public ServiceOverviewViewModel()
        {
            if (SearchText == "")
            {
                Services = serviceRepo.GetItems();
            }
        }
        #endregion

        #region Properties
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;

                if (SearchText == "")
                {
                    Services = serviceRepo.GetItems();
                }
                else
                {
                    Services = serviceRepo.GetItems().Where(c => c.Name.StartsWith(searchText, StringComparison.CurrentCultureIgnoreCase)).ToObservableCollection();
                }
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Service> Services
        {
            get => services;
            private set
            {
                services = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}
