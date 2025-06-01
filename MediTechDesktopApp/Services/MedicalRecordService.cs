using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// Service class to handle MedicalRecord CRUD operations via stored procedures.
    /// </summary>
    public class MedicalRecordService
    {
        private readonly MySqlDbHelper _db;

        public MedicalRecordService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Retrieves all medical records from the database.
        /// Calls stored procedure sp_GetMedicalRecords().
        /// </summary>
        public List<MedicalRecord> GetAllMedicalRecords()
        {
            var dt = _db.ExecuteStoredProcedure("sp_GetMedicalRecords");
            var list = new List<MedicalRecord>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new MedicalRecord
                {
                    RecordId = row["record_id"] != DBNull.Value ? Convert.ToInt32(row["record_id"]) : 0,
                    AppointmentId = row["appointment_id"] != DBNull.Value ? Convert.ToInt32(row["appointment_id"]) : 0,
                    Diagnosis = row["diagnosis"] != DBNull.Value ? row["diagnosis"].ToString() : string.Empty,
                    VisitSummary = row["visit_summary"] != DBNull.Value ? row["visit_summary"].ToString() : string.Empty,
                    DoctorNotes = row["doctor_notes"] != DBNull.Value ? row["doctor_notes"].ToString() : string.Empty,
                    CreatedAt = row["created_at"] != DBNull.Value ? Convert.ToDateTime(row["created_at"]) : DateTime.MinValue
                });
            }

            return list;
        }

        /// <summary>
        /// Adds a new medical record entry.
        /// Calls stored procedure sp_AddMedicalRecord(p_appointment_id, p_diagnosis, p_visit_summary, p_doctor_notes).
        /// </summary>
        public void AddMedicalRecord(MedicalRecord rec)
        {
            var parameters = new Dictionary<string, object>
            {
                { "p_appointment_id", rec.AppointmentId },
                { "p_diagnosis",      rec.Diagnosis },
                { "p_visit_summary",  rec.VisitSummary },
                { "p_doctor_notes",   rec.DoctorNotes }
            };

            _db.ExecuteNonQuery("sp_AddMedicalRecord", parameters);
        }

        /// <summary>
        /// Deletes a medical record by its ID.
        /// Calls stored procedure sp_DeleteMedicalRecord(p_record_id).
        /// </summary>
        public void DeleteMedicalRecord(int recordId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "p_record_id", recordId }
            };

            _db.ExecuteNonQuery("sp_DeleteMedicalRecord", parameters);
        }
    }
}
