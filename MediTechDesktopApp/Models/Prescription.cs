using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents one row in the Prescriptions view, including joined FrequencyName.
    /// </summary>
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public int RecordId { get; set; }
        public string MedicationName { get; set; }
        public string Dosage { get; set; }
        public int FrequencyId { get; set; }
        public string FrequencyName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Instructions { get; set; }

        /// <summary>
        /// For drop‐down: show "Record #ID"
        /// </summary>
        public string DisplayRecord => $"Record {RecordId}";
    }
}
