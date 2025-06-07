

using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// Encapsulates all database operations for Doctors (raw SQL).
    /// </summary>
    public class DoctorService
    {
        private readonly MySqlDbHelper _db;

        public DoctorService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Fetches all Specializations (for populating the ComboBox).
        /// Returns a DataTable with columns (specialization_id, name).
        /// </summary>
        public DataTable GetAllSpecializations()
        {
            string sql = "SELECT specialization_id, name FROM Specializations ORDER BY name;";
            return _db.ExecuteRawQuery(sql);
        }

        /// <summary>
        /// Fetches all Doctors (including specialization name via JOIN).
        /// Returns a List<Doctor> for easy binding.
        /// </summary>
        public List<Doctor> GetAllDoctors()
        {
            var results = new List<Doctor>();

            string sql = @"
                SELECT 
                  d.doctor_id,
                  d.first_name,
                  d.last_name,
                  d.specialization_id,
                  s.name AS specialization_name,
                  d.contact_phone,
                  d.contact_email
                FROM Doctors d
                JOIN Specializations s 
                  ON d.specialization_id = s.specialization_id
                ORDER BY d.last_name, d.first_name;
            ";
            DataTable dt = _db.ExecuteRawQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                results.Add(new Doctor
                {
                    DoctorId = Convert.ToInt32(row["doctor_id"]),
                    FirstName = row["first_name"].ToString(),
                    LastName = row["last_name"].ToString(),
                    SpecializationId = Convert.ToInt32(row["specialization_id"]),
                    SpecializationName = row["specialization_name"].ToString(),
                    ContactPhone = row["contact_phone"].ToString(),
                    ContactEmail = row["contact_email"].ToString()
                });
            }

            return results;
        }

        /// <summary>
        /// Inserts a new Doctor row into the database.
        /// </summary>
        public void AddDoctor(Doctor d)
        {
            string sql = @"
                INSERT INTO Doctors
                  (first_name, last_name, specialization_id, contact_phone, contact_email)
                VALUES
                  (@first_name, @last_name, @specialization_id, @contact_phone, @contact_email);
            ";
            var parameters = new Dictionary<string, object>
            {
                { "first_name",       d.FirstName },
                { "last_name",        d.LastName },
                { "specialization_id", d.SpecializationId },
                { "contact_phone",    d.ContactPhone },
                { "contact_email",    d.ContactEmail }
            };
            _db.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// Updates an existing Doctor’s specialization/phone/email by ID.
        /// </summary>
        public void UpdateDoctor(Doctor d)
        {
            string sql = @"
                UPDATE Doctors
                   SET specialization_id = @specialization_id,
                       contact_phone    = @contact_phone,
                       contact_email    = @contact_email
                 WHERE doctor_id = @doctor_id;
            ";
            var parameters = new Dictionary<string, object>
            {
                { "doctor_id",        d.DoctorId },
                { "specialization_id", d.SpecializationId },
                { "contact_phone",     d.ContactPhone },
                { "contact_email",     d.ContactEmail }
            };
            _db.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// Deletes a Doctor by ID.
        /// </summary>
        public void DeleteDoctor(int doctorId)
        {
            string sql = "DELETE FROM Doctors WHERE doctor_id = @doctor_id;";
            var parameters = new Dictionary<string, object>
            {
                { "doctor_id", doctorId }
            };
            _db.ExecuteNonQuery(sql, parameters);
        }
    }
}
