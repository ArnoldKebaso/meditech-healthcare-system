using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// CRUD for Appointments (joins Patients and Doctors for display).
    /// </summary>
    public class AppointmentService
    {
        private readonly MySqlDbHelper _db;

        public AppointmentService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Returns all appointments joined to Patients and Doctors so we can display names.
        /// </summary>
        public List<Appointment> GetAllAppointments()
        {
            var results = new List<Appointment>();

            string sql = @"
                SELECT 
                  a.appointment_id,
                  a.patient_id,
                  CONCAT(p.first_name, ' ', p.last_name) AS PatientName,
                  a.doctor_id,
                  CONCAT(d.first_name, ' ', d.last_name) AS DoctorName,
                  a.appointment_date,
                  a.status,
                  a.notes
                FROM Appointments a
                JOIN Patients p ON a.patient_id = p.patient_id
                JOIN Doctors d ON a.doctor_id = d.doctor_id
                ORDER BY a.appointment_date DESC;
            ";

            DataTable dt = _db.ExecuteRawQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                results.Add(new Appointment
                {
                    AppointmentId = Convert.ToInt32(row["appointment_id"]),
                    PatientId = Convert.ToInt32(row["patient_id"]),
                    PatientName = row["PatientName"].ToString(),
                    DoctorId = Convert.ToInt32(row["doctor_id"]),
                    DoctorName = row["DoctorName"].ToString(),
                    AppointmentDate = Convert.ToDateTime(row["appointment_date"]),
                    Status = row["status"].ToString(),
                    Notes = row["notes"].ToString()
                });
            }

            return results;
        }

        /// <summary>
        /// Inserts a new appointment via stored procedure sp_AddAppointment.
        /// </summary>
        public void AddAppointment(Appointment a)
        {
            string sql = "CALL sp_AddAppointment(@patient_id, @doctor_id, @appointment_date, @status, @notes);";
            var parms = new Dictionary<string, object>
            {
                {"patient_id",       a.PatientId},
                {"doctor_id",        a.DoctorId},
                {"appointment_date", a.AppointmentDate},
                {"status",           a.Status},
                {"notes",            a.Notes}
            };
            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Updates an existing appointment via stored procedure sp_UpdateAppointment.
        /// </summary>
        public void UpdateAppointment(Appointment a)
        {
            string sql = @"
                CALL sp_UpdateAppointment(
                    @appointment_id,
                    @patient_id,
                    @doctor_id,
                    @appointment_date,
                    @status,
                    @notes
                );
            ";
            var parms = new Dictionary<string, object>
            {
                {"appointment_id",   a.AppointmentId},
                {"patient_id",       a.PatientId},
                {"doctor_id",        a.DoctorId},
                {"appointment_date", a.AppointmentDate},
                {"status",           a.Status},
                {"notes",            a.Notes}
            };
            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Deletes one appointment by ID via sp_DeleteAppointment.
        /// </summary>
        public void DeleteAppointment(int appointmentId)
        {
            string sql = "CALL sp_DeleteAppointment(@appointment_id);";
            var parms = new Dictionary<string, object>
            {
                {"appointment_id", appointmentId}
            };
            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Returns a list of (patient_id, "First Last") for populating Patient ComboBox.
        /// </summary>
        public List<(int id, string name)> GetAllPatientsForCombo()
        {
            var results = new List<(int, string)>();
            string sql = @"
                SELECT patient_id, CONCAT(first_name,' ',last_name) AS FullName
                  FROM Patients
                 ORDER BY last_name, first_name;
            ";
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

        /// <summary>
        /// Returns a list of (doctor_id, "First Last") for populating Doctor ComboBox.
        /// </summary>
        public List<(int id, string name)> GetAllDoctorsForCombo()
        {
            var results = new List<(int, string)>();
            string sql = @"
                SELECT doctor_id, CONCAT(first_name,' ',last_name) AS FullName
                  FROM Doctors
                 ORDER BY last_name, first_name;
            ";
            DataTable dt = _db.ExecuteRawQuery(sql);
            foreach (DataRow row in dt.Rows)
            {
                results.Add((
                    Convert.ToInt32(row["doctor_id"]),
                    row["FullName"].ToString()
                ));
            }
            return results;
        }

        /// <summary>
        /// Helper: fetch one single appointment row (by ID) if needed.
        /// </summary>
        public Appointment GetAppointmentById(int appointmentId)
        {
            // Not strictly needed for our UI. 
            return null;
        }
    }
}
