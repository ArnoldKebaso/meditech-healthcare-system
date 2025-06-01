using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents a payment record (Payments table).
    /// </summary>
    public class Payment
    {
        public int PaymentId { get; set; }
        public int InvoiceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal AmountPaid { get; set; }
        public string Method { get; set; }  // Cash, Card, Insurance, Other
        public string TransactionRef { get; set; }
    }
}
