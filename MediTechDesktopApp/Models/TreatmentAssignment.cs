// File: Models\TreatmentAssignment.cs
using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Maps to the TreatmentAssignments table:
    ///   patient_id, treatment_id, assignment_date, assigned_doctor_id, assigned_nurse_id, notes
    /// </summary>
    public class TreatmentAssignment
    {
        public int PatientId { get; set; }
        public int TreatmentId { get; set; }
        public DateTime AssignmentDate { get; set; }
        public int AssignedDoctorId { get; set; }
        public int AssignedNurseId { get; set; }
        public string Notes { get; set; }

        /// <summary>
        /// Composite Key display (e.g. “Pt:1 / Tr:2 / Date:2025-06-01”)
        /// </summary>
        public string CompositeKey =>
            $"Pt:{PatientId} / Tr:{TreatmentId} / Dt:{AssignmentDate:yyyy-MM-dd}";
    }
}
