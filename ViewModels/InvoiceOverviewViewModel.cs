using Blumen.Models;
using Blumen.Persistence;
using System.Collections.ObjectModel;

namespace Blumen.ViewModels
{
    class InvoiceOverviewViewModel
    {
        #region Fields
        private InvoiceRepo invoiceRepo = new();
        #endregion

        #region Constructors
        public InvoiceOverviewViewModel()
        {
        }
        #endregion

        #region Properties
        public ObservableCollection<Invoice> Invoices { get => invoiceRepo.GetItems(); }
        #endregion

        #region Methods

        #endregion
    }
}
