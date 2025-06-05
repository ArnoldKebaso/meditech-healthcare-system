using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// CRUD for Prescriptions (joins MedicalRecords→FrequencyTypes for display).
    /// </summary>
    public class PrescriptionService
    {
        private readonly MySqlDbHelper _db;

        public PrescriptionService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Returns all prescriptions, joined to FrequencyTypes for display and MedicalRecords (ID).
        /// </summary>
        public List<Prescription> GetAllPrescriptions()
        {
            var results = new List<Prescription>();
            string sql = @"
                SELECT 
                  pr.prescription_id,
                  pr.record_id,
                  pr.medication_name,
                  pr.dosage,
                  pr.frequency_id,
                  f.name           AS FrequencyName,
                  pr.start_date,
                  pr.end_date,
                  pr.instructions
                FROM Prescriptions pr
                JOIN FrequencyTypes f ON pr.frequency_id = f.frequency_id
                ORDER BY pr.prescription_id DESC;
            ";
            DataTable dt = _db.ExecuteRawQuery(sql);
            foreach (DataRow row in dt.Rows)
            {
                results.Add(new Prescription
                {
                    PrescriptionId = Convert.ToInt32(row["prescription_id"]),
                    RecordId = Convert.ToInt32(row["record_id"]),
                    MedicationName = row["medication_name"].ToString(),
                    Dosage = row["dosage"].ToString(),
                    FrequencyId = Convert.ToInt32(row["frequency_id"]),
                    FrequencyName = row["FrequencyName"].ToString(),

                    // If the column is DBNull, set to null; otherwise convert
                    StartDate = row["start_date"] == DBNull.Value
                                ? (DateTime?)null
                                : Convert.ToDateTime(row["start_date"]),

                    EndDate = row["end_date"] == DBNull.Value
                              ? (DateTime?)null
                              : Convert.ToDateTime(row["end_date"]),

                    Instructions = row["instructions"].ToString()
                });
            }
            return results;
        }

        /// <summary>
        /// Inserts a new prescription via sp_AddPrescription.
        /// </summary>
        public void AddPrescription(Prescription pr)
        {
            string sql = @"
                CALL sp_AddPrescription(
                    @record_id,
                    @medication_name,
                    @dosage,
                    @frequency_id,
                    @start_date,
                    @end_date,
                    @instructions
                );
            ";
            var parms = new Dictionary<string, object>
            {
                {"record_id",       pr.RecordId},
                {"medication_name", pr.MedicationName},
                {"dosage",          pr.Dosage},
                {"frequency_id",    pr.FrequencyId},
                // If StartDate is null, pass DBNull.Value
                {"start_date",      pr.StartDate.HasValue ? (object)pr.StartDate.Value : DBNull.Value},
                // If EndDate is null, pass DBNull.Value
                {"end_date",        pr.EndDate.HasValue   ? (object)pr.EndDate.Value   : DBNull.Value},
                {"instructions",    pr.Instructions}
            };
            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Updates a prescription via sp_UpdatePrescription.
        /// </summary>
        public void UpdatePrescription(Prescription pr)
        {
            string sql = @"
                CALL sp_UpdatePrescription(
                    @prescription_id,
                    @medication_name,
                    @dosage,
                    @frequency_id,
                    @start_date,
                    @end_date,
                    @instructions
                );
            ";
            var parms = new Dictionary<string, object>
            {
                {"prescription_id", pr.PrescriptionId},
                {"medication_name", pr.MedicationName},
                {"dosage",          pr.Dosage},
                {"frequency_id",    pr.FrequencyId},
                {"start_date",      pr.StartDate.HasValue ? (object)pr.StartDate.Value : DBNull.Value},
                {"end_date",        pr.EndDate.HasValue   ? (object)pr.EndDate.Value   : DBNull.Value},
                {"instructions",    pr.Instructions}
            };
            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Deletes a prescription via sp_DeletePrescription.
        /// </summary>
        public void DeletePrescription(int prescriptionId)
        {
            string sql = "CALL sp_DeletePrescription(@prescription_id);";
            var parms = new Dictionary<string, object>
            {
                {"prescription_id", prescriptionId}
            };
            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Returns a list of (record_id, "RecordID → Appointment/PatientName") for ComboBox.
        /// </summary>
        public List<(int id, string display)> GetAllMedicalRecordsForCombo()
        {
            var results = new List<(int, string)>();
            string sql = @"
                SELECT 
                  mr.record_id,
                  CONCAT(p.first_name,' ',p.last_name) AS PatientName
                FROM MedicalRecords mr
                JOIN Appointments a ON mr.appointment_id = a.appointment_id
                JOIN Patients p ON a.patient_id = p.patient_id
                ORDER BY mr.created_at DESC;
            ";
            DataTable dt = _db.ExecuteRawQuery(sql);
            foreach (DataRow row in dt.Rows)
            {
                results.Add((
                    Convert.ToInt32(row["record_id"]),
                    $"Record {row["record_id"]} → {row["PatientName"]}"
                ));
            }
            return results;
        }

        /// <summary>
        /// Returns a list of (frequency_id, frequency name) for ComboBox.
        /// </summary>
        public List<(int id, string name)> GetAllFrequencyTypesForCombo()
        {
            var results = new List<(int, string)>();
            string sql = "SELECT frequency_id, name FROM FrequencyTypes ORDER BY name;";
            DataTable dt = _db.ExecuteRawQuery(sql);
            foreach (DataRow row in dt.Rows)
            {
                results.Add((
                    Convert.ToInt32(row["frequency_id"]),
                    row["name"].ToString()
                ));
            }
            return results;
        }
    }
}
