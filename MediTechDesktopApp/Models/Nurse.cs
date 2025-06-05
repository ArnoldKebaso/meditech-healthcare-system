//namespace MediTechDesktopApp.Models
//{
//    /// <summary>
//    /// Nurse model representing the Nurses table (joined with Departments).
//    /// </summary>
//    public class Nurse
//    {
//        public int NurseId { get; set; }

//        public string FirstName { get; set; }

//        public string LastName { get; set; }

//        /// <summary>
//        /// FK to the Departments lookup table.
//        /// </summary>
//        public int DepartmentId { get; set; }

//        /// <summary>
//        /// Human‐readable department name (via JOIN).
//        /// </summary>
//        public string DepartmentName { get; set; }

//        public string ContactPhone { get; set; }

//        public string ContactEmail { get; set; }

//        /// <summary>
//        /// Convenience property for full name.
//        /// </summary>
//        public string FullName => $"{FirstName} {LastName}";
//    }
//}
// File: Models/Nurse.cs

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents one row in the Nurses table (joined with Departments).
    /// </summary>
    public class Nurse
    {
        public int NurseId { get; set; } // database column nurse_id
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; } // foreign key to Departments
        public string DepartmentName { get; set; } // joined column for display
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }

        /// <summary>
        /// Convenience full name.
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";
    }
}
