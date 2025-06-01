using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents a medical record entry (MedicalRecords table).
    /// </summary>
    public class MedicalRecord
    {
        public int RecordId { get; set; }
        public int AppointmentId { get; set; }
        public string Diagnosis { get; set; }
        public string VisitSummary { get; set; }
        public string DoctorNotes { get; set; }
        public DateTime CreatedAt { get; set; }  // Defaulted in DB
    }
}
