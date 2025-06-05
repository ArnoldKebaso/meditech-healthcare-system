using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// CRUD for Departments (simple lookup table).
    /// </summary>
    public class DepartmentService
    {
        private readonly MySqlDbHelper _db;

        public DepartmentService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Fetch all departments in alphabetical order.
        /// </summary>
        public List<Department> GetAllDepartments()
        {
            var results = new List<Department>();
            string sql = @"
                SELECT department_id, name 
                  FROM Departments
                 ORDER BY name;
            ";
            DataTable dt = _db.ExecuteRawQuery(sql);
            foreach (DataRow row in dt.Rows)
            {
                results.Add(new Department
                {
                    DepartmentId = Convert.ToInt32(row["department_id"]),
                    Name = row["name"].ToString()
                });
            }
            return results;
        }

        /// <summary>
        /// Add a new department.
        /// </summary>
        public void AddDepartment(Department d)
        {
            string sql = @"
                INSERT INTO Departments (name)
                VALUES (@name);
            ";
            var parms = new Dictionary<string, object>
            {
                { "name", d.Name }
            };
            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Update an existing department.
        /// </summary>
        public void UpdateDepartment(Department d)
        {
            string sql = @"
                UPDATE Departments
                   SET name = @name
                 WHERE department_id = @department_id;
            ";
            var parms = new Dictionary<string, object>
            {
                { "name",          d.Name },
                { "department_id", d.DepartmentId }
            };
            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Delete a department by ID.
        /// </summary>
        public void DeleteDepartment(int departmentId)
        {
            string sql = @"
                DELETE FROM Departments
                 WHERE department_id = @department_id;
            ";
            var parms = new Dictionary<string, object>
            {
                { "department_id", departmentId }
            };
            _db.ExecuteNonQuery(sql, parms);
        }
    }
}
