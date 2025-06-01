using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// Service class to handle Invoice CRUD operations via stored procedures.
    /// </summary>
    public class InvoiceService
    {
        private readonly MySqlDbHelper _db;

        public InvoiceService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Retrieves all invoices from the database.
        /// Calls stored procedure sp_GetInvoices().
        /// </summary>
        public List<Invoice> GetAllInvoices()
        {
            var dt = _db.ExecuteStoredProcedure("sp_GetInvoices");
            var list = new List<Invoice>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Invoice
                {
                    InvoiceId = row["invoice_id"] != DBNull.Value ? Convert.ToInt32(row["invoice_id"]) : 0,
                    PatientId = row["patient_id"] != DBNull.Value ? Convert.ToInt32(row["patient_id"]) : 0,
                    InvoiceDate = row["invoice_date"] != DBNull.Value ? Convert.ToDateTime(row["invoice_date"]) : DateTime.MinValue,
                    TotalAmount = row["total_amount"] != DBNull.Value ? Convert.ToDecimal(row["total_amount"]) : 0m,
                    Status = row["status"] != DBNull.Value ? row["status"].ToString() : string.Empty
                });
            }

            return list;
        }

        /// <summary>
        /// Adds a new invoice record.
        /// Calls stored procedure sp_AddInvoice(p_id, p_date, p_amount, p_status).
        /// </summary>
        public void AddInvoice(Invoice inv)
        {
            var parameters = new Dictionary<string, object>
            {
                { "p_patient", inv.PatientId },
                { "p_inv_date", inv.InvoiceDate },
                { "p_tot_amt", inv.TotalAmount },
                { "p_st", inv.Status }
            };

            _db.ExecuteNonQuery("sp_AddInvoice", parameters);
        }

        /// <summary>
        /// Updates only the status of an existing invoice.
        /// Calls stored procedure sp_UpdateInvoiceStatus(p_inv_id, new_status).
        /// </summary>
        public void UpdateInvoiceStatus(int invoiceId, string newStatus)
        {
            var parameters = new Dictionary<string, object>
            {
                { "inv_id", invoiceId },
                { "new_status", newStatus }
            };

            _db.ExecuteNonQuery("sp_UpdateInvoiceStatus", parameters);
        }

        /// <summary>
        /// Deletes an invoice by ID.
        /// Calls stored procedure sp_DeleteInvoice(p_inv_id).
        /// </summary>
        public void DeleteInvoice(int invoiceId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "inv_id", invoiceId }
            };

            _db.ExecuteNonQuery("sp_DeleteInvoice", parameters);
        }
    }
}
