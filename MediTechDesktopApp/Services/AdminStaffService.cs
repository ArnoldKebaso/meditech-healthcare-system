// File: Services\AdminStaffService.cs
using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// Service class for managing admin‐staff CRUD operations.
    /// </summary>
    public class AdminStaffService
    {
        private readonly MySqlDbHelper _db;

        public AdminStaffService()
        {
            _db = new MySqlDbHelper();
        }

        public List<AdminStaff> GetAllAdmins()
        {
            var table = _db.ExecuteStoredProcedure("sp_GetAdmins");
            var list = new List<AdminStaff>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new AdminStaff
                {
                    StaffId = row["staff_id"] != DBNull.Value ? Convert.ToInt32(row["staff_id"]) : 0,
                    FirstName = row["first_name"]?.ToString(),
                    LastName = row["last_name"]?.ToString(),
                    Role = row["role"]?.ToString(),
                    ContactPhone = row["contact_phone"]?.ToString(),
                    ContactEmail = row["contact_email"]?.ToString()
                });
            }

            return list;
        }

        public void AddAdmin(AdminStaff a)
        {
            var parameters = new Dictionary<string, object>
            {
                { "a_first", a.FirstName },
                { "a_last",  a.LastName  },
                { "a_role",  a.Role      },
                { "a_phone", a.ContactPhone },
                { "a_email", a.ContactEmail }
            };

            _db.ExecuteNonQuery("sp_AddAdmin", parameters);
        }

        public void UpdateAdmin(AdminStaff a)
        {
            var parameters = new Dictionary<string, object>
            {
                { "a_id",    a.StaffId },
                { "a_role",  a.Role    },
                { "a_phone", a.ContactPhone },
                { "a_email", a.ContactEmail }
            };

            _db.ExecuteNonQuery("sp_UpdateAdmin", parameters);
        }

        public void DeleteAdmin(int staffId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "a_id", staffId }
            };

            _db.ExecuteNonQuery("sp_DeleteAdmin", parameters);
        }
    }
}
