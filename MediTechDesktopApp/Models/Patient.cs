using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Patient model representing the Patients table in DB.
    /// </summary>
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}