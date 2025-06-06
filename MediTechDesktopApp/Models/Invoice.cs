// Models/Invoice.cs
using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents one Invoice row, both for inserting/updating and for reading (joined columns).
    /// </summary>
    public class Invoice
    {
        // Primary‐key column
        public int InvoiceId { get; set; }

        // FK → Patients table
        public int PatientId { get; set; }

        // Joined/display column
        public string PatientFullName { get; set; }   // alias from JOIN

        // Invoice details
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
}
