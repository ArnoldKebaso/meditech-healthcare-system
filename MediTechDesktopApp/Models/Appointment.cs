using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents one row in the Appointments view, including joined PatientName and DoctorName.
    /// </summary>
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

        /// <summary>
        /// Concatenate ID + name for display (e.g. "5 - Alice Anderson")
        /// </summary>
        public string Display => $"{AppointmentId} - {PatientName}";
    }
}
