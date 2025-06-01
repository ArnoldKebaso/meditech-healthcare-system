// File: Models\Doctor.cs
using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Doctor model representing the Doctors table in the database.
    /// </summary>
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }

        /// <summary>
        /// Convenience property to show "FirstName LastName".
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";
    }
}
