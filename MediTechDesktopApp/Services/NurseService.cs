// File: Services\NurseService.cs
using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// Service class for managing nurse‐related database operations.
    /// </summary>
    public class NurseService
    {
        private readonly MySqlDbHelper _db;

        public NurseService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Retrieves all nurses from the database.
        /// </summary>
        public List<Nurse> GetAllNurses()
        {
            var table = _db.ExecuteStoredProcedure("sp_GetNurses");
            var nurses = new List<Nurse>();

            foreach (DataRow row in table.Rows)
            {
                nurses.Add(new Nurse
                {
                    NurseId = row["nurse_id"] != DBNull.Value ? Convert.ToInt32(row["nurse_id"]) : 0,
                    FirstName = row["first_name"]?.ToString(),
                    LastName = row["last_name"]?.ToString(),
                    Department = row["department"]?.ToString(),
                    ContactPhone = row["contact_phone"]?.ToString(),
                    ContactEmail = row["contact_email"]?.ToString()
                });
            }

            return nurses;
        }

        /// <summary>
        /// Inserts a new nurse into the database.
        /// </summary>
        public void AddNurse(Nurse n)
        {
            var parameters = new Dictionary<string, object>
            {
                { "n_first", n.FirstName },
                { "n_last",  n.LastName },
                { "n_dept",  n.Department },
                { "n_phone", n.ContactPhone },
                { "n_email", n.ContactEmail }
            };

            _db.ExecuteNonQuery("sp_AddNurse", parameters);
        }

        /// <summary>
        /// Updates an existing nurse’s department, phone, or email.
        /// </summary>
        public void UpdateNurse(Nurse n)
        {
            var parameters = new Dictionary<string, object>
            {
                { "n_id",    n.NurseId },
                { "n_dept",  n.Department },
                { "n_phone", n.ContactPhone },
                { "n_email", n.ContactEmail }
            };

            _db.ExecuteNonQuery("sp_UpdateNurse", parameters);
        }

        /// <summary>
        /// Deletes a nurse by ID.
        /// </summary>
        public void DeleteNurse(int nurseId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "n_id", nurseId }
            };

            _db.ExecuteNonQuery("sp_DeleteNurse", parameters);
        }
    }
}
