using System;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;
using System.Collections.Generic;
using System.Data;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// Service class for managing patient-related database operations.
    /// </summary>
    public class PatientService
    {
        private readonly MySqlDbHelper _db;

        public PatientService()
        {
            _db = new MySqlDbHelper();
        }

        public List<Patient> GetAllPatients()
        {
            var table = _db.ExecuteStoredProcedure("sp_GetPatients");
            var patients = new List<Patient>();

            foreach (DataRow row in table.Rows)
            {
                patients.Add(new Patient
                {
                    PatientId = row["patient_id"] != DBNull.Value ? Convert.ToInt32(row["patient_id"]) : 0,
                    FirstName = row["first_name"]?.ToString(),
                    LastName = row["last_name"]?.ToString(),
                    DateOfBirth = row["date_of_birth"] != DBNull.Value ? Convert.ToDateTime(row["date_of_birth"]) : DateTime.MinValue,
                    Gender = row["gender"]?.ToString(),
                    ContactPhone = row["contact_phone"]?.ToString(),
                    ContactEmail = row["contact_email"]?.ToString(),
                    Address = row["address"]?.ToString()
                });
            }

            return patients;
        }

        public void AddPatient(Patient p)
        {
            var parameters = new Dictionary<string, object>
            {
                { "p_first", p.FirstName },
                { "p_last", p.LastName },
                { "p_dob", p.DateOfBirth },
                { "p_gender", p.Gender },
                { "p_phone", p.ContactPhone },
                { "p_email", p.ContactEmail },
                { "p_addr", p.Address }
            };

            _db.ExecuteNonQuery("sp_AddPatient", parameters);
        }
    }
}