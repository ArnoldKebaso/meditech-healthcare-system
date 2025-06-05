// File: Services/PaymentService.cs
using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// CRUD for Payments.
    /// </summary>
    public class PaymentService
    {
        private readonly MySqlDbHelper _db;

        public PaymentService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Returns all payments joined with invoice label and patient name.
        /// </summary>
        public List<Payment> GetAllPayments()
        {
            var results = new List<Payment>();
            string sql = @"
                SELECT
                  pay.payment_id,
                  pay.invoice_id,
                  CONCAT('#', CAST(pay.invoice_id AS CHAR)) AS InvoiceLabel,
                  i.patient_id,
                  CONCAT(p.first_name, ' ', p.last_name) AS patient_name,
                  pay.payment_date,
                  pay.amount_paid,
                  pay.method,
                  pay.transaction_ref
                FROM Payments pay
                  JOIN Invoices i ON pay.invoice_id = i.invoice_id
                  JOIN Patients p ON i.patient_id = p.patient_id
                ORDER BY pay.payment_date DESC;
            ";
            DataTable dt = _db.ExecuteRawQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                results.Add(new Payment
                {
                    PaymentId = Convert.ToInt32(row["payment_id"]),
                    InvoiceId = Convert.ToInt32(row["invoice_id"]),
                    InvoiceLabel = row["InvoiceLabel"].ToString(),
                    PatientFullName = row["patient_name"].ToString(),
                    PaymentDate = Convert.ToDateTime(row["payment_date"]),
                    AmountPaid = Convert.ToDecimal(row["amount_paid"]),
                    Method = row["method"].ToString(),
                    TransactionRef = row["transaction_ref"].ToString()
                });
            }

            return results;
        }

        /// <summary>
        /// Inserts a new payment.
        /// </summary>
        public void AddPayment(int invoiceId, DateTime paymentDate, decimal amountPaid, string method, string txRef)
        {
            string sql = @"
                CALL sp_AddPayment(
                  @invoice_id, @payment_date, @amount_paid, @method, @transaction_ref
                );
            ";
            var parms = new Dictionary<string, object>
            {
                { "invoice_id",      invoiceId },
                { "payment_date",    paymentDate },
                { "amount_paid",     amountPaid },
                { "method",          method },
                { "transaction_ref", txRef }
            };
            _db.ExecuteRawQuery(sql, parms);
        }

        /// <summary>
        /// Updates amount, method, or transaction_ref of an existing payment.
        /// </summary>
        public void UpdatePayment(int paymentId, decimal amountPaid, string method, string txRef)
        {
            string sql = @"
                CALL sp_UpdatePayment(
                  @payment_id, @amount_paid, @method, @transaction_ref
                );
            ";
            var parms = new Dictionary<string, object>
            {
                { "payment_id",      paymentId },
                { "amount_paid",     amountPaid },
                { "method",          method },
                { "transaction_ref", txRef }
            };
            _db.ExecuteRawQuery(sql, parms);
        }

        /// <summary>
        /// Deletes a payment.
        /// </summary>
        public void DeletePayment(int paymentId)
        {
            string sql = @"CALL sp_DeletePayment(@payment_id);";
            var parms = new Dictionary<string, object>
            {
                { "payment_id", paymentId }
            };
            _db.ExecuteRawQuery(sql, parms);
        }

        /// <summary>
        /// Returns a list of all invoice IDs (labelled “#N”) to populate dropdown.
        /// </summary>
        public List<(int Id, string Label)> GetAllInvoicesForCombo()
        {
            var output = new List<(int, string)>();
            string sql = @"
                SELECT 
                  invoice_id,
                  CONCAT('#', CAST(invoice_id AS CHAR)) AS Label
                FROM Invoices
                ORDER BY invoice_id DESC;
            ";
            DataTable dt = _db.ExecuteRawQuery(sql);
            foreach (DataRow row in dt.Rows)
            {
                output.Add((
                    Convert.ToInt32(row["invoice_id"]),
                    row["Label"].ToString()
                ));
            }
            return output;
        }
    }
}
