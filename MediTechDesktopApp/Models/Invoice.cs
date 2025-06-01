using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents an invoice record (Invoices table).
    /// </summary>
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int PatientId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }  // Pending, Paid, Cancelled

        /// <summary>
        /// Optional convenience property for display (not strictly required).
        /// </summary>
        public string DisplayStatus => Status;
    }
}
