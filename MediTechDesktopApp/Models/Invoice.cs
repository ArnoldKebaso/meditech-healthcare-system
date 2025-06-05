// File: Models/Invoice.cs
using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents a single row in the Invoices view.
    /// </summary>
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int PatientId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }  // 'Pending', 'Paid', 'Cancelled'

        // Joined column:
        public string PatientFullName { get; set; }  // alias: patient_name
    }
}
