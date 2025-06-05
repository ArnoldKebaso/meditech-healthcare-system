using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// CRUD for Invoices table, plus patient lookups.
    /// </summary>
    public class InvoiceService
    {
        private readonly MySqlDbHelper _db;

        public InvoiceService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Returns all invoices joined to Patients so we can display full name.
        /// </summary>
        public List<Invoice> GetAllInvoices()
        {
            var results = new List<Invoice>();

            string sql = @"
                SELECT 
                  i.invoice_id,
                  i.patient_id,
                  i.invoice_date,
                  i.total_amount,
                  i.status,
                  CONCAT(p.first_name, ' ', p.last_name) AS PatientFullName
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
                    InvoiceDate = Convert.ToDateTime(row["invoice_date"]),
                    TotalAmount = Convert.ToDecimal(row["total_amount"]),
                    Status = row["status"].ToString(),
                    PatientFullName = row["PatientFullName"].ToString()
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
                { "patient_id",   inv.PatientId },
                { "invoice_date", inv.InvoiceDate },
                { "total_amount", inv.TotalAmount },
                { "status",       inv.Status }
            };
            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Updates the status, date or amount of an existing invoice.
        /// </summary>
        public void UpdateInvoice(Invoice inv)
        {
            string sql = @"
                UPDATE Invoices
                   SET patient_id   = @patient_id,
                       invoice_date = @invoice_date,
                       total_amount = @total_amount,
                       status       = @status
                 WHERE invoice_id = @invoice_id;
            ";
            var parms = new Dictionary<string, object>
            {
                { "invoice_id",   inv.InvoiceId },
                { "patient_id",   inv.PatientId },
                { "invoice_date", inv.InvoiceDate },
                { "total_amount", inv.TotalAmount },
                { "status",       inv.Status }
            };
            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Deletes an invoice by ID (and cascades or restricts if children exist).
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
        /// Returns a list of all patients for populating cbPatient.
        /// </summary>
        public List<(int id, string fullName)> GetAllPatientsForCombo()
        {
            string sql = @"
                SELECT 
                  patient_id, 
                  CONCAT(first_name, ' ', last_name) AS FullName
                FROM Patients
                ORDER BY last_name, first_name;
            ";
            var results = new List<(int, string)>();
            DataTable dt = _db.ExecuteRawQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                results.Add((
                    Convert.ToInt32(row["patient_id"]),
                    row["FullName"].ToString()
                ));
            }
            return results;
        }
    }
}
