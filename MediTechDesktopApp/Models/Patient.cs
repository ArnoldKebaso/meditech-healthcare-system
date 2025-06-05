

using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents a single row in the Patients table.
    /// </summary>
    public class Patient
    {
        public int PatientId { get; set; }   // matches database column patient_id
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }   // values: "Male", "Female", "Other"
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Address { get; set; }

        /// <summary>
        /// Concatenate first and last name for display.
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";
    }
}
