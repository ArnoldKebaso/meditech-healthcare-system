using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// CRUD for MedicalRecords (joins Appointments→Patients/Doctors for display).
    /// </summary>
    public class MedicalRecordService
    {
        private readonly MySqlDbHelper _db;

        public MedicalRecordService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Returns all medical records with joined PatientName and DoctorName.
        /// </summary>
        public List<MedicalRecord> GetAllMedicalRecords()
        {
            var results = new List<MedicalRecord>();

            string sql = @"
                SELECT 
                  mr.record_id,
                  mr.appointment_id,
                  CONCAT(p.first_name,' ',p.last_name) AS PatientName,
                  CONCAT(d.first_name,' ',d.last_name) AS DoctorName,
                  mr.diagnosis,
                  mr.visit_summary,
                  mr.doctor_notes,
                  mr.created_at
                FROM MedicalRecords mr
                JOIN Appointments a ON mr.appointment_id = a.appointment_id
                JOIN Patients p     ON a.patient_id     = p.patient_id
                JOIN Doctors d      ON a.doctor_id      = d.doctor_id
                ORDER BY mr.created_at DESC;
            ";

            DataTable dt = _db.ExecuteRawQuery(sql);
            foreach (DataRow row in dt.Rows)
            {
                results.Add(new MedicalRecord
                {
                    RecordId = Convert.ToInt32(row["record_id"]),
                    AppointmentId = Convert.ToInt32(row["appointment_id"]),
                    PatientName = row["PatientName"].ToString(),
                    DoctorName = row["DoctorName"].ToString(),
                    Diagnosis = row["diagnosis"].ToString(),
                    VisitSummary = row["visit_summary"].ToString(),
                    DoctorNotes = row["doctor_notes"].ToString(),
                    CreatedAt = Convert.ToDateTime(row["created_at"])
                });
            }
            return results;
        }

        /// <summary>
        /// Inserts a new medical record via sp_AddMedicalRecord.
        /// </summary>
        public void AddMedicalRecord(MedicalRecord mr)
        {
            string sql = @"
                CALL sp_AddMedicalRecord(
                    @appointment_id,
                    @diagnosis,
                    @visit_summary,
                    @doctor_notes
                );
            ";
            var parms = new Dictionary<string, object>
            {
                {"appointment_id", mr.AppointmentId},
                {"diagnosis", mr.Diagnosis},
                {"visit_summary", mr.VisitSummary},
                {"doctor_notes", mr.DoctorNotes}
            };
            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Updates an existing medical record via sp_UpdateMedicalRecord.
        /// </summary>
        public void UpdateMedicalRecord(MedicalRecord mr)
        {
            string sql = @"
                CALL sp_UpdateMedicalRecord(
                    @record_id,
                    @diagnosis,
                    @visit_summary,
                    @doctor_notes
                );
            ";
            var parms = new Dictionary<string, object>
            {
                {"record_id",     mr.RecordId},
                {"diagnosis",     mr.Diagnosis},
                {"visit_summary", mr.VisitSummary},
                {"doctor_notes",  mr.DoctorNotes}
            };
            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Deletes one medical record via sp_DeleteMedicalRecord.
        /// </summary>
        public void DeleteMedicalRecord(int recordId)
        {
            string sql = "CALL sp_DeleteMedicalRecord(@record_id);";
            var parms = new Dictionary<string, object>
            {
                {"record_id", recordId}
            };
            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Returns a list of (appointment_id, "AppointmentID → PatientName") for ComboBox.
        /// </summary>
        public List<(int id, string display)> GetAllAppointmentsForCombo()
        {
            var results = new List<(int, string)>();
            string sql = @"
                SELECT 
                  a.appointment_id,
                  CONCAT(p.first_name,' ',p.last_name) AS PatientName
                FROM Appointments a
                JOIN Patients p ON a.patient_id = p.patient_id
                ORDER BY a.appointment_date DESC;
            ";
            DataTable dt = _db.ExecuteRawQuery(sql);
            foreach (DataRow row in dt.Rows)
            {
                results.Add((
                    Convert.ToInt32(row["appointment_id"]),
                    $"{row["appointment_id"]} → {row["PatientName"]}"
                ));
            }
            return results;
        }
    }
}
