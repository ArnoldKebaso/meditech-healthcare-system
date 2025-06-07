
namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents one row in the Doctors table (joined with Specializations).
    /// </summary>
    public class Doctor
    {
        public int DoctorId { get; set; } // database column doctor_id
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SpecializationId { get; set; } // foreign key to Specializations
        public string SpecializationName { get; set; } // joined column for display
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }

        /// <summary>
        /// Convenient full name for display in combiners, etc.
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";
    }
}
