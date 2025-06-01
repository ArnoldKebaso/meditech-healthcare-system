using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// Service class to handle Payment CRUD operations via stored procedures.
    /// </summary>
    public class PaymentService
    {
        private readonly MySqlDbHelper _db;

        public PaymentService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Retrieves all payments from the database.
        /// Calls stored procedure sp_GetPayments().
        /// </summary>
        public List<Payment> GetAllPayments()
        {
            var dt = _db.ExecuteStoredProcedure("sp_GetPayments");
            var list = new List<Payment>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Payment
                {
                    PaymentId = row["payment_id"] != DBNull.Value ? Convert.ToInt32(row["payment_id"]) : 0,
                    InvoiceId = row["invoice_id"] != DBNull.Value ? Convert.ToInt32(row["invoice_id"]) : 0,
                    PaymentDate = row["payment_date"] != DBNull.Value ? Convert.ToDateTime(row["payment_date"]) : DateTime.MinValue,
                    AmountPaid = row["amount_paid"] != DBNull.Value ? Convert.ToDecimal(row["amount_paid"]) : 0m,
                    Method = row["method"] != DBNull.Value ? row["method"].ToString() : string.Empty,
                    TransactionRef = row["transaction_ref"] != DBNull.Value ? row["transaction_ref"].ToString() : string.Empty
                });
            }

            return list;
        }

        /// <summary>
        /// Adds a new payment record.
        /// Calls stored procedure sp_AddPayment(p_inv_id, p_pay_date, p_amt_paid, p_method, p_tx_ref).
        /// </summary>
        public void AddPayment(Payment pay)
        {
            var parameters = new Dictionary<string, object>
            {
                { "inv_id",  pay.InvoiceId },
                { "pay_date", pay.PaymentDate },
                { "amt_paid", pay.AmountPaid },
                { "method",   pay.Method },
                { "tx_ref",   pay.TransactionRef }
            };

            _db.ExecuteNonQuery("sp_AddPayment", parameters);
        }

        /// <summary>
        /// Deletes a payment by ID.
        /// Calls stored procedure sp_DeletePayment(p_pay_id).
        /// </summary>
        public void DeletePayment(int paymentId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "pay_id", paymentId }
            };

            _db.ExecuteNonQuery("sp_DeletePayment", parameters);
        }
    }
}
