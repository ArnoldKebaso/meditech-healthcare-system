// File: Models/AdminStaff.cs
namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents one row in AdminStaff (joined with StaffRoles to get RoleName).
    /// </summary>
    public class AdminStaff
    {
        public int StaffId { get; set; } // PK
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; } // FK → StaffRoles.RoleId
        public string RoleName { get; set; } // populated by the service via a JOIN
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }

        /// <summary>
        /// For ComboBoxes or quick display, e.g. “John Smith”.
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";
    }
}
