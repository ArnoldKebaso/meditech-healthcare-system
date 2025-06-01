using System.Collections.ObjectModel;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// ViewModel to handle Invoice CRUD and expose an ObservableCollection of Invoice.
    /// </summary>
    public class InvoiceViewModel
    {
        private readonly InvoiceService _service;

        /// <summary>
        /// Collection bound to InvoiceView’s DataGrid.
        /// </summary>
        public ObservableCollection<Invoice> Invoices { get; } = new ObservableCollection<Invoice>();

        /// <summary>
        /// The currently selected invoice (if needed).
        /// </summary>
        public Invoice SelectedInvoice { get; set; } = new Invoice();

        public InvoiceViewModel()
        {
            _service = new InvoiceService();
            LoadInvoices();
        }

        /// <summary>
        /// Reloads all invoices from the DB.
        /// </summary>
        public void LoadInvoices()
        {
            Invoices.Clear();
            var all = _service.GetAllInvoices();
            foreach (var inv in all)
            {
                Invoices.Add(inv);
            }
        }

        /// <summary>
        /// Inserts a new invoice via the Service and reloads list.
        /// </summary>
        public void AddInvoice(Invoice newInv)
        {
            if (newInv == null) return;
            _service.AddInvoice(newInv);
            LoadInvoices();
        }

        /// <summary>
        /// Deletes the specified invoice.
        /// </summary>
        public void DeleteInvoice(int invoiceId)
        {
            _service.DeleteInvoice(invoiceId);
            LoadInvoices();
        }

        /// <summary>
        /// Updates the status of an invoice.
        /// </summary>
        public void UpdateInvoiceStatus(int invoiceId, string newStatus)
        {
            _service.UpdateInvoiceStatus(invoiceId, newStatus);
            LoadInvoices();
        }
    }
}
