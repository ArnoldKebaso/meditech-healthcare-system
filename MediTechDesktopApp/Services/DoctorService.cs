//using System;
//using System.Collections.Generic;
//using System.Data;
//using MediTechDesktopApp.DataAccess;
//using MediTechDesktopApp.Models;

//namespace MediTechDesktopApp.Services
//{
//    /// <summary>
//    /// Service class for managing doctor‐related database operations.
//    /// Uses:
//    ///   • sp_GetSpecializations
//    ///   • sp_GetDoctors / raw SQL join
//    ///   • sp_AddDoctor
//    ///   • sp_UpdateDoctor
//    ///   • sp_DeleteDoctor
//    /// </summary>
//    public class DoctorService
//    {
//        private readonly MySqlDbHelper _db;

//        public DoctorService()
//        {
//            _db = new MySqlDbHelper();
//        }

//        /// <summary>
//        /// Retrieves all specializations (ID + name) from the database.
//        /// Calls sp_GetSpecializations (no parameters).
//        /// </summary>
//        public DataTable GetAllSpecializations()
//        {
//            try
//            {
//                return _db.ExecuteStoredProcedure("sp_GetSpecializations", new Dictionary<string, object>());
//            }
//            catch
//            {
//                throw;
//            }
//        }

//        /// <summary>
//        /// Retrieves all doctors (including joined specialization name).
//        /// Uses raw SQL JOIN between Doctors and Specializations.
//        /// Maps each DataRow into a Doctor object.
//        /// </summary>
//        public List<Doctor> GetAllDoctors()
//        {
//            var doctors = new List<Doctor>();
//            try
//            {
//                string sql = @"
//                    SELECT 
//                      d.doctor_id,
//                      d.first_name,
//                      d.last_name,
//                      d.specialization_id,
//                      s.name AS specialization_name,
//                      d.contact_phone,
//                      d.contact_email
//                    FROM Doctors d
//                    JOIN Specializations s
//                      ON d.specialization_id = s.specialization_id;
//                ";
//                DataTable table = _db.ExecuteRawQuery(sql);
//                foreach (DataRow row in table.Rows)
//                {
//                    doctors.Add(new Doctor
//                    {
//                        DoctorId = row["doctor_id"] != DBNull.Value ? Convert.ToInt32(row["doctor_id"]) : 0,
//                        FirstName = row["first_name"]?.ToString(),
//                        LastName = row["last_name"]?.ToString(),
//                        SpecializationId = row["specialization_id"] != DBNull.Value ? Convert.ToInt32(row["specialization_id"]) : 0,
//                        SpecializationName = row["specialization_name"]?.ToString(),
//                        ContactPhone = row["contact_phone"]?.ToString(),
//                        ContactEmail = row["contact_email"]?.ToString()
//                    });
//                }
//            }
//            catch
//            {
//                throw;
//            }
//            return doctors;
//        }

//        /// <summary>
//        /// Adds a new doctor to the database.
//        /// Calls sp_AddDoctor with parameters: d_first, d_last, d_spec_id, d_phone, d_email.
//        /// </summary>
//        public void AddDoctor(Doctor doc)
//        {
//            var parameters = new Dictionary<string, object>
//            {
//                { "d_first",    doc.FirstName       },
//                { "d_last",     doc.LastName        },
//                { "d_spec_id",  doc.SpecializationId},
//                { "d_phone",    doc.ContactPhone    },
//                { "d_email",    doc.ContactEmail    }
//            };
//            _db.ExecuteStoredProcedureNonQuery("sp_AddDoctor", parameters);
//        }

//        /// <summary>
//        /// Updates an existing doctor’s specialization/phone/email.
//        /// Calls sp_UpdateDoctor with parameters: d_id, d_spec_id, d_phone, d_email.
//        /// </summary>
//        public void UpdateDoctor(Doctor doc)
//        {
//            var parameters = new Dictionary<string, object>
//            {
//                { "d_id",       doc.DoctorId        },
//                { "d_spec_id",  doc.SpecializationId},
//                { "d_phone",    doc.ContactPhone    },
//                { "d_email",    doc.ContactEmail    }
//            };
//            _db.ExecuteStoredProcedureNonQuery("sp_UpdateDoctor", parameters);
//        }

//        /// <summary>
//        /// Deletes a doctor by ID.
//        /// Calls sp_DeleteDoctor with parameter: d_id.
//        /// </summary>
//        public void DeleteDoctor(int doctorId)
//        {
//            var parameters = new Dictionary<string, object>
//            {
//                { "d_id", doctorId }
//            };
//            _db.ExecuteStoredProcedureNonQuery("sp_DeleteDoctor", parameters);
//        }
//    }
//}
// File: Services/DoctorService.cs

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
