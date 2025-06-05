

using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// Encapsulates all database operations for Patients (no stored procs).
    /// </summary>
    public class PatientService
    {
        private readonly MySqlDbHelper _db;

        public PatientService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Fetches all Patients from the database.
        /// Returns a List<Patient> for easy binding.
        /// </summary>
        public List<Patient> GetAllPatients()
        {
            var results = new List<Patient>();

            // 1) Raw SELECT from Patients
            string sql = @"
                SELECT 
                   patient_id, 
                   first_name, 
                   last_name, 
                   date_of_birth, 
                   gender, 
                   contact_phone, 
                   contact_email, 
                   address
                FROM Patients
                ORDER BY last_name, first_name;
            ";

            DataTable dt = _db.ExecuteRawQuery(sql);

            // 2) Map each DataRow to a Patient object
            foreach (DataRow row in dt.Rows)
            {
                results.Add(new Patient
                {
                    PatientId = Convert.ToInt32(row["patient_id"]),
                    FirstName = row["first_name"].ToString(),
                    LastName = row["last_name"].ToString(),
                    DateOfBirth = Convert.ToDateTime(row["date_of_birth"]),
                    Gender = row["gender"].ToString(),
                    ContactPhone = row["contact_phone"].ToString(),
                    ContactEmail = row["contact_email"].ToString(),
                    Address = row["address"].ToString()
                });
            }

            return results;
        }

        /// <summary>
        /// Inserts a new Patient into the database.
        /// </summary>
        public void AddPatient(Patient p)
        {
            string sql = @"
                INSERT INTO Patients
                  (first_name, last_name, date_of_birth, gender, contact_phone, contact_email, address)
                VALUES
                  (@first_name, @last_name, @date_of_birth, @gender, @contact_phone, @contact_email, @address);
            ";
            var parameters = new Dictionary<string, object>
            {
                { "first_name",     p.FirstName },
                { "last_name",      p.LastName },
                { "date_of_birth",  p.DateOfBirth },
                { "gender",         p.Gender },
                { "contact_phone",  p.ContactPhone },
                { "contact_email",  p.ContactEmail },
                { "address",        p.Address }
            };
            _db.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// Updates an existing Patient’s contact info in the database.
        /// (We assume names, DOB & gender are not editable after creation.)
        /// </summary>
        public void UpdatePatient(Patient p)
        {
            string sql = @"
                UPDATE Patients
                   SET contact_phone = @contact_phone,
                       contact_email = @contact_email,
                       address       = @address
                 WHERE patient_id = @patient_id;
            ";
            var parameters = new Dictionary<string, object>
            {
                { "patient_id",    p.PatientId },
                { "contact_phone", p.ContactPhone },
                { "contact_email", p.ContactEmail },
                { "address",       p.Address }
            };
            _db.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// Deletes a Patient by ID.
        /// </summary>
        public void DeletePatient(int patientId)
        {
            string sql = "DELETE FROM Patients WHERE patient_id = @patient_id;";
            var parameters = new Dictionary<string, object>
            {
                { "patient_id", patientId }
            };
            _db.ExecuteNonQuery(sql, parameters);
        }
    }
}
