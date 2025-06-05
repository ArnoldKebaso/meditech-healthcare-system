// File: Models/Payment.cs
using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents a single row in the Payments view.
    /// </summary>
    public class Payment
    {
        public int PaymentId { get; set; }
        public int InvoiceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal AmountPaid { get; set; }
        public string Method { get; set; }  // 'Cash','Card','Insurance','Other'
        public string TransactionRef { get; set; }

        // Joined columns (for display only):
        public string InvoiceLabel { get; set; }   // e.g. "#5"
        public string PatientFullName { get; set; }   // alias: patient_name
    }
}
