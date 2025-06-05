

using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// Encapsulates all database operations for Nurses (raw SQL).
    /// </summary>
    public class NurseService
    {
        private readonly MySqlDbHelper _db;

        public NurseService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Fetches all Departments (for populating the ComboBox).
        /// Returns a DataTable with (department_id, name).
        /// </summary>
        public DataTable GetAllDepartments()
        {
            string sql = "SELECT department_id, name FROM Departments ORDER BY name;";
            return _db.ExecuteRawQuery(sql);
        }

        /// <summary>
        /// Fetches all Nurses (including department name via JOIN).
        /// Returns a List<Nurse> for easy binding.
        /// </summary>
        public List<Nurse> GetAllNurses()
        {
            var results = new List<Nurse>();

            string sql = @"
                SELECT 
                  n.nurse_id,
                  n.first_name,
                  n.last_name,
                  n.department_id,
                  d.name AS department_name,
                  n.contact_phone,
                  n.contact_email
                FROM Nurses n
                JOIN Departments d
                  ON n.department_id = d.department_id
                ORDER BY n.last_name, n.first_name;
            ";
            DataTable dt = _db.ExecuteRawQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                results.Add(new Nurse
                {
                    NurseId = Convert.ToInt32(row["nurse_id"]),
                    FirstName = row["first_name"].ToString(),
                    LastName = row["last_name"].ToString(),
                    DepartmentId = Convert.ToInt32(row["department_id"]),
                    DepartmentName = row["department_name"].ToString(),
                    ContactPhone = row["contact_phone"].ToString(),
                    ContactEmail = row["contact_email"].ToString()
                });
            }

            return results;
        }

        /// <summary>
        /// Inserts a new Nurse into the database.
        /// </summary>
        public void AddNurse(Nurse n)
        {
            string sql = @"
                INSERT INTO Nurses
                  (first_name, last_name, department_id, contact_phone, contact_email)
                VALUES
                  (@first_name, @last_name, @department_id, @contact_phone, @contact_email);
            ";
            var parameters = new Dictionary<string, object>
            {
                { "first_name",     n.FirstName },
                { "last_name",      n.LastName },
                { "department_id",  n.DepartmentId },
                { "contact_phone",  n.ContactPhone },
                { "contact_email",  n.ContactEmail }
            };
            _db.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// Updates an existing Nurse’s department/phone/email in the database.
        /// </summary>
        public void UpdateNurse(Nurse n)
        {
            string sql = @"
                UPDATE Nurses
                   SET department_id  = @department_id,
                       contact_phone  = @contact_phone,
                       contact_email  = @contact_email
                 WHERE nurse_id = @nurse_id;
            ";
            var parameters = new Dictionary<string, object>
            {
                { "nurse_id",      n.NurseId },
                { "department_id", n.DepartmentId },
                { "contact_phone", n.ContactPhone },
                { "contact_email", n.ContactEmail }
            };
            _db.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// Deletes a Nurse by ID.
        /// </summary>
        public void DeleteNurse(int nurseId)
        {
            string sql = "DELETE FROM Nurses WHERE nurse_id = @nurse_id;";
            var parameters = new Dictionary<string, object>
            {
                { "nurse_id", nurseId }
            };
            _db.ExecuteNonQuery(sql, parameters);
        }
    }
}
