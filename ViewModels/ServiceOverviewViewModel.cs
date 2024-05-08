using Blumen.Models;
using Blumen.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blumen.ViewModels
{
    class ServiceOverviewViewModel
    {
        #region Fields
        private ServiceRepo serviceRepo = new();
        #endregion

        #region Constructors
        #endregion

        #region Properties
        public ObservableCollection<Service> Services { get => serviceRepo.GetItems(); }
        #endregion

        #region Methods
        #endregion
    }
}
