using Blumen.Models;
using Blumen.Persistence;
using System.Collections.ObjectModel;

namespace Blumen.ViewModels
{
    public class ProductTypeOverviewViewModel : ObservableObject
    {
        #region Fields
        private ProductTypeRepo productTypeRepo = new();

        private string searchText = "";
        private ObservableCollection<ProductType> productTypes;
        #endregion

        #region Constructors
        public ProductTypeOverviewViewModel()
        {
            if (SearchText == "")
            {
                ProductTypes = productTypeRepo.GetItems();
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
                    ProductTypes = productTypeRepo.GetItems();
                }
                else
                {
                    ProductTypes = productTypeRepo.GetItems().Where(c => c.Name.StartsWith(searchText, StringComparison.CurrentCultureIgnoreCase)).ToObservableCollection();
                }
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
        #endregion

        #region Methods
        #endregion
    }
}
