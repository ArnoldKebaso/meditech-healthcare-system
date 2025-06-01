// File: Services\TreatmentAssignmentService.cs
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// Service to CRUD TreatmentAssignments (calls sp_GetAssignments, sp_AddAssignment, etc.)
    /// </summary>
    public class TreatmentAssignmentService
    {
        private readonly MySqlDbHelper _db;

        public TreatmentAssignmentService()
        {
            _db = new MySqlDbHelper();
        }

        public List<TreatmentAssignment> GetAllAssignments()
        {
            var table = _db.ExecuteStoredProcedure("sp_GetAssignments");
            var list = new List<TreatmentAssignment>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new TreatmentAssignment
                {
                    PatientId = Convert.ToInt32(row["patient_id"]),
                    TreatmentId = Convert.ToInt32(row["treatment_id"]),
                    AssignmentDate = Convert.ToDateTime(row["assignment_date"]),
                    AssignedDoctorId = Convert.ToInt32(row["assigned_doctor_id"]),
                    AssignedNurseId = Convert.ToInt32(row["assigned_nurse_id"]),
                    Notes = row["notes"]?.ToString()
                });
            }

            return list;
        }

        public void AddAssignment(TreatmentAssignment ta)
        {
            var parameters = new Dictionary<string, object>
            {
                { "p_id",      ta.PatientId },
                { "t_id",      ta.TreatmentId },
                { "a_date",    ta.AssignmentDate },
                { "doc_id",    ta.AssignedDoctorId },
                { "nurse_id",  ta.AssignedNurseId },
                { "a_notes",   ta.Notes }
            };

            _db.ExecuteNonQuery("sp_AddAssignment", parameters);
        }
    }
}
