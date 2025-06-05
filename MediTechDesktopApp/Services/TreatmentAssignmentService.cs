using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// CRUD for TreatmentAssignments (with some joined columns for display).
    /// </summary>
    public class TreatmentAssignmentService
    {
        private readonly MySqlDbHelper _db;

        public TreatmentAssignmentService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Returns all TreatmentAssignments joined to Patients/Doctors/Nurses/Treatments 
        /// so we can display the names in the grid easily.
        /// </summary>
        public List<TreatmentAssignment> GetAllAssignments()
        {
            var results = new List<TreatmentAssignment>();

            string sql = @"
                SELECT 
                  ta.patient_id,
                  ta.treatment_id,
                  ta.assignment_date,
                  ta.assigned_doctor_id,
                  ta.assigned_nurse_id,
                  ta.notes,

                  -- Join to Patients just to get full name:
                  CONCAT(p.first_name, ' ', p.last_name) AS PatientFullName,

                  -- Join to Treatments (just in case you want treatment name):
                  t.name AS TreatmentName,

                  -- Join to Doctors and Nurses for full names:
                  CONCAT(d.first_name, ' ', d.last_name) AS DoctorFullName,
                  CONCAT(n.first_name, ' ', n.last_name) AS NurseFullName

                FROM TreatmentAssignments ta
                JOIN Patients   p ON ta.patient_id = p.patient_id
                JOIN Treatments t ON ta.treatment_id = t.treatment_id
                JOIN Doctors    d ON ta.assigned_doctor_id = d.doctor_id
                JOIN Nurses     n ON ta.assigned_nurse_id = n.nurse_id
                ORDER BY ta.assignment_date DESC;
            ";

            DataTable dt = _db.ExecuteRawQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                results.Add(new TreatmentAssignment
                {
                    PatientId = Convert.ToInt32(row["patient_id"]),
                    TreatmentId = Convert.ToInt32(row["treatment_id"]),
                    AssignmentDate = Convert.ToDateTime(row["assignment_date"]),
                    AssignedDoctorId = Convert.ToInt32(row["assigned_doctor_id"]),
                    AssignedNurseId = Convert.ToInt32(row["assigned_nurse_id"]),
                    Notes = row["notes"].ToString(),

                    PatientFullName = row["PatientFullName"].ToString(),
                    TreatmentName = row["TreatmentName"].ToString(),
                    DoctorFullName = row["DoctorFullName"].ToString(),
                    NurseFullName = row["NurseFullName"].ToString()
                });
            }

            return results;
        }

        /// <summary>
        /// Inserts a new TreatmentAssignment row. 
        /// </summary>
        public void AddAssignment(TreatmentAssignment ta)
        {
            string sql = @"
                INSERT INTO TreatmentAssignments
                  (patient_id, treatment_id, assignment_date, assigned_doctor_id, assigned_nurse_id, notes)
                VALUES
                  (@patient_id, @treatment_id, @assignment_date, @assigned_doctor_id, @assigned_nurse_id, @notes);
            ";

            var parms = new Dictionary<string, object>
            {
                { "patient_id",         ta.PatientId },
                { "treatment_id",       ta.TreatmentId },
                { "assignment_date",    ta.AssignmentDate },
                { "assigned_doctor_id", ta.AssignedDoctorId },
                { "assigned_nurse_id",  ta.AssignedNurseId },
                { "notes",              ta.Notes }
            };

            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Updates the Notes of an existing Assignment. 
        /// (Because PK is composite, you must pass in all three key fields.)
        /// </summary>
        public void UpdateAssignment(TreatmentAssignment ta)
        {
            string sql = @"
                UPDATE TreatmentAssignments
                   SET notes = @notes
                 WHERE patient_id         = @patient_id
                   AND treatment_id       = @treatment_id
                   AND assignment_date    = @assignment_date;
            ";

            var parms = new Dictionary<string, object>
            {
                { "notes",           ta.Notes },
                { "patient_id",      ta.PatientId },
                { "treatment_id",    ta.TreatmentId },
                { "assignment_date", ta.AssignmentDate }
            };

            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Deletes an existing TreatmentAssignment using the composite PK.
        /// </summary>
        public void DeleteAssignment(int patientId, int treatmentId, DateTime assignmentDate)
        {
            string sql = @"
                DELETE FROM TreatmentAssignments
                 WHERE patient_id      = @patient_id
                   AND treatment_id    = @treatment_id
                   AND assignment_date = @assignment_date;
            ";

            var parms = new Dictionary<string, object>
            {
                { "patient_id",      patientId },
                { "treatment_id",    treatmentId },
                { "assignment_date", assignmentDate }
            };

            _db.ExecuteNonQuery(sql, parms);
        }
    }
}
