using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents one row in the Treatments table.
    /// </summary>
    public class Treatment
    {
        public int TreatmentId { get; set; }       // PK → treatments.treatment_id
        public string Name { get; set; }           // treatments.name
        public string Description { get; set; }    // treatments.description
        public DateTime CreatedAt { get; set; }    // treatments.created_at
    }
}
