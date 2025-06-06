// Services/InvoiceService.cs
using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// CRUD operations for the Invoices table, plus a helper to fetch Patients for a ComboBox.
    /// </summary>
    public class InvoiceService
    {
        private readonly MySqlDbHelper _db;

        public InvoiceService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Returns all Invoices joined with Patients so we can display the patient’s full name.
        /// </summary>
        public List<Invoice> GetAllInvoices()
        {
            var results = new List<Invoice>();

            string sql = @"
                SELECT
                  i.invoice_id,
                  i.patient_id,
                  CONCAT(p.first_name, ' ', p.last_name) AS PatientFullName,
                  i.invoice_date   AS InvoiceDate,
                  i.total_amount   AS TotalAmount,
                  i.status         AS Status
                FROM Invoices i
                JOIN Patients p ON i.patient_id = p.patient_id
                ORDER BY i.invoice_date DESC;
            ";

            DataTable dt = _db.ExecuteRawQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                results.Add(new Invoice
                {
                    InvoiceId = Convert.ToInt32(row["invoice_id"]),
                    PatientId = Convert.ToInt32(row["patient_id"]),
                    PatientFullName = row["PatientFullName"].ToString(),
                    InvoiceDate = Convert.ToDateTime(row["InvoiceDate"]),
                    TotalAmount = Convert.ToDecimal(row["TotalAmount"]),
                    Status = row["Status"].ToString()
                });
            }

            return results;
        }

        /// <summary>
        /// Inserts a new invoice. 
        /// </summary>
        public void AddInvoice(Invoice inv)
        {
            string sql = @"
                INSERT INTO Invoices
                  (patient_id, invoice_date, total_amount, status)
                VALUES
                  (@patient_id, @invoice_date, @total_amount, @status);
            ";

            var parms = new Dictionary<string, object>
            {
                { "patient_id",  inv.PatientId },
                { "invoice_date", inv.InvoiceDate },
                { "total_amount", inv.TotalAmount },
                { "status",      inv.Status }
            };

            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Deletes an invoice by its primary key (InvoiceId).
        /// </summary>
        public void DeleteInvoice(int invoiceId)
        {
            string sql = @"
                DELETE FROM Invoices
                 WHERE invoice_id = @invoice_id;
            ";
            var parms = new Dictionary<string, object>
            {
                { "invoice_id", invoiceId }
            };
            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Updates an existing invoice’s date, amount, and status.
        /// (We do not allow changing patient_id here for simplicity.)
        /// </summary>
        public void UpdateInvoice(Invoice inv)
        {
            string sql = @"
                UPDATE Invoices
                SET invoice_date = @invoice_date,
                    total_amount = @total_amount,
                    status = @status
                WHERE invoice_id = @invoice_id;
            ";
            var parms = new Dictionary<string, object>
            {
                { "invoice_id",   inv.InvoiceId },
                { "invoice_date", inv.InvoiceDate },
                { "total_amount", inv.TotalAmount },
                { "status",       inv.Status }
            };
            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Returns a list of ComboItem(id, fullName) for populating the Patient ComboBox.
        /// </summary>
        public List<ComboItem> GetAllPatientsForCombo()
        {
            string sql = @"
                SELECT 
                  patient_id, 
                  CONCAT(first_name, ' ', last_name) AS FullName
                FROM Patients
                ORDER BY last_name, first_name;
            ";
            var results = new List<ComboItem>();
            DataTable dt = _db.ExecuteRawQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                results.Add(new ComboItem
                {
                    Id = Convert.ToInt32(row["patient_id"]),
                    Name = row["FullName"].ToString()
                });
            }

            return results;
        }
    }
}
