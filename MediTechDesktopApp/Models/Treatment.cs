// File: Models\Treatment.cs
using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents a treatment type.
    /// </summary>
    public class Treatment
    {
        public int TreatmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public override string ToString() => $"{Name}";
    }
}
