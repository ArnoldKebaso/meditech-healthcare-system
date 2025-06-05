using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// Encapsulates all CRUD for AdminStaff (no SPs, raw SQL).
    /// </summary>
    public class AdminStaffService
    {
        private readonly MySqlDbHelper _db;

        public AdminStaffService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Returns all AdminStaff rows joined to StaffRoles so we have RoleName available.
        /// </summary>
        public List<AdminStaff> GetAllAdmins()
        {
            var results = new List<AdminStaff>();

            string sql = @"
                SELECT 
                  a.staff_id,
                  a.first_name,
                  a.last_name,
                  a.role_id,
                  r.name         AS role_name,
                  a.contact_phone,
                  a.contact_email
                FROM AdminStaff a
                JOIN StaffRoles r ON a.role_id = r.role_id
                ORDER BY a.last_name, a.first_name;
            ";

            DataTable dt = _db.ExecuteRawQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                results.Add(new AdminStaff
                {
                    StaffId = Convert.ToInt32(row["staff_id"]),
                    FirstName = row["first_name"].ToString(),
                    LastName = row["last_name"].ToString(),
                    RoleId = Convert.ToInt32(row["role_id"]),
                    RoleName = row["role_name"].ToString(),
                    ContactPhone = row["contact_phone"].ToString(),
                    ContactEmail = row["contact_email"].ToString()
                });
            }

            return results;
        }

        /// <summary>
        /// Inserts a new AdminStaff row.
        /// </summary>
        public void AddAdmin(AdminStaff a)
        {
            string sql = @"
                INSERT INTO AdminStaff
                  (first_name, last_name, role_id, contact_phone, contact_email)
                VALUES
                  (@first_name, @last_name, @role_id, @contact_phone, @contact_email);
            ";

            var parms = new Dictionary<string, object>
            {
                { "first_name",     a.FirstName },
                { "last_name",      a.LastName },
                { "role_id",        a.RoleId },
                { "contact_phone",  a.ContactPhone },
                { "contact_email",  a.ContactEmail }
            };

            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Updates an existing AdminStaff’s contact info or role.
        /// </summary>
        public void UpdateAdmin(AdminStaff a)
        {
            string sql = @"
                UPDATE AdminStaff
                   SET first_name    = @first_name,
                       last_name     = @last_name,
                       role_id       = @role_id,
                       contact_phone = @contact_phone,
                       contact_email = @contact_email
                 WHERE staff_id = @staff_id;
            ";

            var parms = new Dictionary<string, object>
            {
                { "first_name",     a.FirstName },
                { "last_name",      a.LastName },
                { "role_id",        a.RoleId },
                { "contact_phone",  a.ContactPhone },
                { "contact_email",  a.ContactEmail },
                { "staff_id",       a.StaffId }
            };

            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Deletes an AdminStaff row by its StaffId.
        /// </summary>
        public void DeleteAdmin(int staffId)
        {
            string sql = "DELETE FROM AdminStaff WHERE staff_id = @staff_id;";

            var parms = new Dictionary<string, object>
            {
                { "staff_id", staffId }
            };

            _db.ExecuteNonQuery(sql, parms);
        }
    }
}
