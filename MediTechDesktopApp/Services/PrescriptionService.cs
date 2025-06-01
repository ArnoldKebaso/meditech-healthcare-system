using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// Service class to handle Prescription CRUD operations via stored procedures.
    /// </summary>
    public class PrescriptionService
    {
        private readonly MySqlDbHelper _db;

        public PrescriptionService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Retrieves all prescriptions from the database.
        /// Calls stored procedure sp_GetPrescriptions().
        /// </summary>
        public List<Prescription> GetAllPrescriptions()
        {
            var dt = _db.ExecuteStoredProcedure("sp_GetPrescriptions");
            var list = new List<Prescription>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Prescription
                {
                    PrescriptionId = row["prescription_id"] != DBNull.Value ? Convert.ToInt32(row["prescription_id"]) : 0,
                    RecordId = row["record_id"] != DBNull.Value ? Convert.ToInt32(row["record_id"]) : 0,
                    MedicationName = row["medication_name"] != DBNull.Value ? row["medication_name"].ToString() : string.Empty,
                    Dosage = row["dosage"] != DBNull.Value ? row["dosage"].ToString() : string.Empty,
                    Frequency = row["frequency"] != DBNull.Value ? row["frequency"].ToString() : string.Empty,
                    StartDate = row["start_date"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["start_date"]) : null,
                    EndDate = row["end_date"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["end_date"]) : null,
                    Instructions = row["instructions"] != DBNull.Value ? row["instructions"].ToString() : string.Empty
                });
            }

            return list;
        }

        /// <summary>
        /// Adds a new prescription entry.
        /// Calls stored procedure sp_AddPrescription(p_record_id, p_medication_name, p_dosage, p_frequency, p_start_date, p_end_date, p_instructions).
        /// </summary>
        public void AddPrescription(Prescription pres)
        {
            var parameters = new Dictionary<string, object>
            {
                { "p_record_id",       pres.RecordId },
                { "p_medication_name", pres.MedicationName },
                { "p_dosage",          pres.Dosage },
                { "p_frequency",       pres.Frequency },
                { "p_start_date",      pres.StartDate },
                { "p_end_date",        pres.EndDate },
                { "p_instructions",    pres.Instructions }
            };

            _db.ExecuteNonQuery("sp_AddPrescription", parameters);
        }

        /// <summary>
        /// Deletes a prescription by ID.
        /// Calls stored procedure sp_DeletePrescription(p_prescription_id).
        /// </summary>
        public void DeletePrescription(int prescriptionId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "p_prescription_id", prescriptionId }
            };

            _db.ExecuteNonQuery("sp_DeletePrescription", parameters);
        }
    }
}
