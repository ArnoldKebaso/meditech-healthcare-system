// File: Models\Appointment.cs
using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents a single patient‐doctor appointment.
    /// </summary>
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

        /// <summary>
        /// Combines patient & doctor IDs for display.
        /// </summary>
        public string DisplayInfo => $"P:{PatientId} → D:{DoctorId} on {AppointmentDate}";
    }
}
