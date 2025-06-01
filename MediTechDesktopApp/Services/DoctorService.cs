// File: Services\DoctorService.cs
using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// Service class for managing doctor‐related database operations.
    /// </summary>
    public class DoctorService
    {
        private readonly MySqlDbHelper _db;

        public DoctorService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Retrieves all doctors from the database.
        /// </summary>
        public List<Doctor> GetAllDoctors()
        {
            var table = _db.ExecuteStoredProcedure("sp_GetDoctors");
            var doctors = new List<Doctor>();

            foreach (DataRow row in table.Rows)
            {
                doctors.Add(new Doctor
                {
                    DoctorId = row["doctor_id"] != DBNull.Value ? Convert.ToInt32(row["doctor_id"]) : 0,
                    FirstName = row["first_name"]?.ToString(),
                    LastName = row["last_name"]?.ToString(),
                    Specialization = row["specialization"]?.ToString(),
                    ContactPhone = row["contact_phone"]?.ToString(),
                    ContactEmail = row["contact_email"]?.ToString()
                });
            }

            return doctors;
        }

        /// <summary>
        /// Inserts a new doctor into the database.
        /// </summary>
        public void AddDoctor(Doctor d)
        {
            var parameters = new Dictionary<string, object>
            {
                { "d_first", d.FirstName },
                { "d_last",  d.LastName },
                { "d_spec",  d.Specialization },
                { "d_phone", d.ContactPhone },
                { "d_email", d.ContactEmail }
            };

            _db.ExecuteNonQuery("sp_AddDoctor", parameters);
        }

        /// <summary>
        /// Updates an existing doctor's specialization, phone, or email.
        /// </summary>
        public void UpdateDoctor(Doctor d)
        {
            var parameters = new Dictionary<string, object>
            {
                { "d_id",    d.DoctorId },
                { "d_spec",  d.Specialization },
                { "d_phone", d.ContactPhone },
                { "d_email", d.ContactEmail }
            };

            _db.ExecuteNonQuery("sp_UpdateDoctor", parameters);
        }

        /// <summary>
        /// Deletes a doctor by ID.
        /// </summary>
        public void DeleteDoctor(int doctorId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "d_id", doctorId }
            };

            _db.ExecuteNonQuery("sp_DeleteDoctor", parameters);
        }
    }
}
