// Models/Payment.cs
using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents one Payment row, both for inserting/updating and for reading (joined columns).
    /// </summary>
    public class Payment
    {
        // Primary‐key column
        public int PaymentId { get; set; }

        // FK → Invoices table
        public int InvoiceId { get; set; }

        // Joined/display column (e.g. "Inv 12 – Alice Kamau")
        public string InvoiceDisplay { get; set; }

        // Payment details
        public DateTime PaymentDate { get; set; }
        public decimal AmountPaid { get; set; }
        public string Method { get; set; }
    }
}
