using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents one row in the MedicalRecords view, including joined PatientName and DoctorName.
    /// </summary>
    public class MedicalRecord
    {
        public int RecordId { get; set; }
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string Diagnosis { get; set; }
        public string VisitSummary { get; set; }
        public string DoctorNotes { get; set; }
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// For drop‐down: show "Appointment #ID → PatientName"
        /// </summary>
        public string DisplayAppointment => $"{AppointmentId} → {PatientName}";
    }
}
