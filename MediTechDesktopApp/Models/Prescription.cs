using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents a prescription entry (Prescriptions table).
    /// </summary>
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public int RecordId { get; set; }  // 1:1 link to MedicalRecord
        public string MedicationName { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Instructions { get; set; }
    }
}
