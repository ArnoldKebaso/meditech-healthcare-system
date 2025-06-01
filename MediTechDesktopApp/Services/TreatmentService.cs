// File: Services\TreatmentService.cs
using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// Service class for managing treatment‐type CRUD operations.
    /// </summary>
    public class TreatmentService
    {
        private readonly MySqlDbHelper _db;

        public TreatmentService()
        {
            _db = new MySqlDbHelper();
        }

        public List<Treatment> GetAllTreatments()
        {
            var table = _db.ExecuteStoredProcedure("sp_GetTreatments");
            var list = new List<Treatment>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new Treatment
                {
                    TreatmentId = row["treatment_id"] != DBNull.Value ? Convert.ToInt32(row["treatment_id"]) : 0,
                    Name = row["name"]?.ToString(),
                    Description = row["description"]?.ToString(),
                    CreatedAt = row["created_at"] != DBNull.Value ? Convert.ToDateTime(row["created_at"]) : DateTime.MinValue
                });
            }

            return list;
        }

        public void AddTreatment(Treatment t)
        {
            var parameters = new Dictionary<string, object>
            {
                { "t_name", t.Name },
                { "t_desc", t.Description }
            };

            _db.ExecuteNonQuery("sp_AddTreatment", parameters);
        }

        public void UpdateTreatment(Treatment t)
        {
            var parameters = new Dictionary<string, object>
            {
                { "t_id",   t.TreatmentId },
                { "t_name", t.Name        },
                { "t_desc", t.Description }
            };

            _db.ExecuteNonQuery("sp_UpdateTreatment", parameters);
        }

        public void DeleteTreatment(int treatmentId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "t_id", treatmentId }
            };

            _db.ExecuteNonQuery("sp_DeleteTreatment", parameters);
        }
    }
}
