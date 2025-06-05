using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents one row in TreatmentAssignments (composite PK).
    /// </summary>
    public class TreatmentAssignment
    {
        public int PatientId { get; set; }              // FK → treatmentassignments.patient_id
        public int TreatmentId { get; set; }            // FK → treatmentassignments.treatment_id
        public DateTime AssignmentDate { get; set; }    // treatmentassignments.assignment_date
        public int AssignedDoctorId { get; set; }       // treatmentassignments.assigned_doctor_id
        public int AssignedNurseId { get; set; }        // treatmentassignments.assigned_nurse_id
        public string Notes { get; set; }               // treatmentassignments.notes

        // The UI might want “PatientFullName” etc. We’ll set those in the ViewModel if needed.
        public string PatientFullName { get; set; }
        public string DoctorFullName { get; set; }
        public string NurseFullName { get; set; }
        public string TreatmentName { get; set; }
    }
}
