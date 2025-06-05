using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// CRUD for the Treatments table (no SPs—raw SQL).
    /// </summary>
    public class TreatmentService
    {
        private readonly MySqlDbHelper _db;

        public TreatmentService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// SELECT * FROM Treatments ORDER BY name.
        /// </summary>
        public List<Treatment> GetAllTreatments()
        {
            var results = new List<Treatment>();

            string sql = @"
                SELECT 
                  treatment_id,
                  name,
                  description,
                  created_at
                FROM Treatments
                ORDER BY name;
            ";

            DataTable dt = _db.ExecuteRawQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                results.Add(new Treatment
                {
                    TreatmentId = Convert.ToInt32(row["treatment_id"]),
                    Name = row["name"].ToString(),
                    Description = row["description"].ToString(),
                    CreatedAt = Convert.ToDateTime(row["created_at"])
                });
            }

            return results;
        }

        /// <summary>
        /// Insert a new row in Treatments (name + description).
        /// </summary>
        public void AddTreatment(Treatment t)
        {
            string sql = @"
                INSERT INTO Treatments
                  (name, description)
                VALUES
                  (@name, @description);
            ";

            var parms = new Dictionary<string, object>
            {
                { "name",        t.Name },
                { "description", t.Description }
            };

            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Update name/description for an existing Treatment.
        /// </summary>
        public void UpdateTreatment(Treatment t)
        {
            string sql = @"
                UPDATE Treatments
                   SET name        = @name,
                       description = @description
                 WHERE treatment_id = @treatment_id;
            ";

            var parms = new Dictionary<string, object>
            {
                { "name",         t.Name },
                { "description",  t.Description },
                { "treatment_id", t.TreatmentId }
            };

            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Delete a Treatment by its PK. (Will fail if FK constraints exist; catch that in the UI.)
        /// </summary>
        public void DeleteTreatment(int treatmentId)
        {
            string sql = "DELETE FROM Treatments WHERE treatment_id = @treatment_id;";

            var parms = new Dictionary<string, object>
            {
                { "treatment_id", treatmentId }
            };

            _db.ExecuteNonQuery(sql, parms);
        }
    }
}
