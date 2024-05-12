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
