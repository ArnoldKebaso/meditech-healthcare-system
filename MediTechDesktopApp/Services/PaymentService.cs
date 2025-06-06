// Services/PaymentService.cs
using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// CRUD operations for the Payments table, plus helper to fetch Invoices for a ComboBox.
    /// </summary>
    public class PaymentService
    {
        private readonly MySqlDbHelper _db;

        public PaymentService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Returns all Payments joined with Invoices and Patients so we can display an Invoice label.
        /// </summary>
        public List<Payment> GetAllPayments()
        {
            var results = new List<Payment>();

            string sql = @"
                SELECT
                  pay.payment_id,
                  pay.invoice_id,
                  CONCAT('Inv ', i.invoice_id, ' – ', p.first_name, ' ', p.last_name) AS InvoiceDisplay,
                  pay.payment_date   AS PaymentDate,
                  pay.amount_paid    AS AmountPaid,
                  pay.method         AS Method
                FROM Payments pay
                JOIN Invoices i   ON pay.invoice_id = i.invoice_id
                JOIN Patients p   ON i.patient_id = p.patient_id
                ORDER BY pay.payment_date DESC;
            ";

            DataTable dt = _db.ExecuteRawQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                results.Add(new Payment
                {
                    PaymentId = Convert.ToInt32(row["payment_id"]),
                    InvoiceId = Convert.ToInt32(row["invoice_id"]),
                    InvoiceDisplay = row["InvoiceDisplay"].ToString(),
                    PaymentDate = Convert.ToDateTime(row["PaymentDate"]),
                    AmountPaid = Convert.ToDecimal(row["AmountPaid"]),
                    Method = row["Method"].ToString()
                });
            }

            return results;
        }

        /// <summary>
        /// Inserts a new payment.
        /// </summary>
        public void AddPayment(Payment pay)
        {
            string sql = @"
                INSERT INTO Payments
                  (invoice_id, payment_date, amount_paid, method)
                VALUES
                  (@invoice_id, @payment_date, @amount_paid, @method);
            ";

            var parms = new Dictionary<string, object>
            {
                { "invoice_id",   pay.InvoiceId },
                { "payment_date", pay.PaymentDate },
                { "amount_paid",  pay.AmountPaid },
                { "method",       pay.Method }
            };

            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Updates an existing payment’s date, amount, and method.
        /// (We do not allow changing invoice_id here for simplicity.)
        /// </summary>
        public void UpdatePayment(Payment pay)
        {
            string sql = @"
                UPDATE Payments
                SET payment_date = @payment_date,
                    amount_paid  = @amount_paid,
                    method       = @method
                WHERE payment_id = @payment_id;
            ";

            var parms = new Dictionary<string, object>
            {
                { "payment_id",   pay.PaymentId },
                { "payment_date", pay.PaymentDate },
                { "amount_paid",  pay.AmountPaid },
                { "method",       pay.Method }
            };

            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Deletes a payment by its primary key (PaymentId).
        /// </summary>
        public void DeletePayment(int paymentId)
        {
            string sql = @"
                DELETE FROM Payments
                 WHERE payment_id = @payment_id;
            ";
            var parms = new Dictionary<string, object>
            {
                { "payment_id", paymentId }
            };
            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Returns a list of ComboItem(id, InvoiceDisplay) for populating the Invoice ComboBox.
        /// </summary>
        public List<ComboItem> GetAllInvoicesForCombo()
        {
            string sql = @"
                SELECT 
                  i.invoice_id, 
                  CONCAT('Inv ', i.invoice_id, ' – ', p.first_name, ' ', p.last_name) AS Display
                FROM Invoices i
                JOIN Patients p ON i.patient_id = p.patient_id
                ORDER BY i.invoice_date DESC;
            ";

            var results = new List<ComboItem>();
            DataTable dt = _db.ExecuteRawQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                results.Add(new ComboItem
                {
                    Id = Convert.ToInt32(row["invoice_id"]),
                    Name = row["Display"].ToString()
                });
            }

            return results;
        }
    }
}
