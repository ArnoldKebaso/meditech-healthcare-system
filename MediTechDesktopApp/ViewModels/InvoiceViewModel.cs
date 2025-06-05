// File: ViewModels\InvoiceViewModel.cs
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// ViewModel to expose Invoice list and CRUD methods for binding in InvoiceView.xaml.
    /// </summary>
    public class InvoiceViewModel
    {
        private readonly InvoiceService _service;

        public ObservableCollection<Invoice> Invoices { get; private set; }
            = new ObservableCollection<Invoice>();

        public Invoice SelectedInvoice { get; set; } = new Invoice();

        public InvoiceViewModel()
        {
            _service = new InvoiceService();
            LoadInvoices();
        }

        /// <summary>
        /// Loads all invoices from DB into the ObservableCollection.
        /// </summary>
        public void LoadInvoices()
        {
            Invoices.Clear();
            var list = _service.GetAllInvoices();
            foreach (var inv in list)
            {
                Invoices.Add(inv);
            }
        }

        /// <summary>
        /// Adds a new invoice to the DB and reloads the collection.
        /// </summary>
        public void AddInvoice(Invoice inv)
        {
            if (inv == null) return;
            _service.AddInvoice(inv);
            LoadInvoices();
        }

        /// <summary>
        /// Updates only the status of an existing invoice.
        /// </summary>
        public void UpdateInvoiceStatus(int invoiceId, string newStatus)
        {
            _service.UpdateInvoiceStatus(invoiceId, newStatus);
            LoadInvoices();
        }

        /// <summary>
        /// Deletes a selected invoice by ID.
        /// </summary>
        public void DeleteInvoice(int invoiceId)
        {
            _service.DeleteInvoice(invoiceId);
            LoadInvoices();
        }

        /// <summary>
        /// Returns newline-separated invoice IDs for quick testing.
        /// </summary>
        public string GetInvoiceIds()
        {
            return string.Join("\n", Invoices.Select(i => i.InvoiceId));
        }
    }
}
