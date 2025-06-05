using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// Service to load all rows from the StaffRoles lookup table.
    /// </summary>
    public class StaffRoleService
    {
        private readonly MySqlDbHelper _db;

        public StaffRoleService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Returns a List<StaffRole> containing every role in staffroles.
        /// </summary>
        public List<StaffRole> GetAllRoles()
        {
            var results = new List<StaffRole>();

            // 1) Raw SELECT
            string sql = @"
                SELECT role_id, name
                  FROM StaffRoles
                 ORDER BY name;
            ";

            DataTable dt = _db.ExecuteRawQuery(sql);

            // 2) Map each DataRow → StaffRole
            foreach (DataRow row in dt.Rows)
            {
                results.Add(new StaffRole
                {
                    RoleId = Convert.ToInt32(row["role_id"]),
                    Name = row["name"].ToString()
                });
            }

            return results;
        }
    }
}
