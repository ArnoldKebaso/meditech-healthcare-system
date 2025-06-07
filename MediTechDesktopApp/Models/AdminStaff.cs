// File: Models/AdminStaff.cs
namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents one row in AdminStaff (joined with StaffRoles to get RoleName).
    /// </summary>
    /// 
    /// /// <summary>
    /// Represents one record in the AdminStaff table.
    /// Fields:
    ///  • AdminStaffId (PK, auto‐increment)
    ///  • FirstName
    ///  • LastName
    ///  • Role (e.g. “Receptionist”, “Billing Clerk”, etc.)
    ///  • Phone
    ///  • Email
    /// </summary>
    public class AdminStaff

    {
        /// <summary>
        /// Primary key. Auto‐incremented by the database.
        /// </summary>
        public int StaffId { get; set; } // PK
        /// <summary>
        /// The staff member’s first name.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The staff member’s last name.
        /// </summary>
        public string LastName { get; set; }
        public int RoleId { get; set; } // FK → StaffRoles.RoleId

        /// <summary>
        /// The staff member’s role or job title (e.g., “Receptionist”).
        /// </summary>
        public string RoleName { get; set; } // populated by the service via a JOIN
        /// <summary>
        /// Contact phone number for the staff member.
        /// </summary>
        public string ContactPhone { get; set; }
        /// <summary>
        /// Contact email for the staff member.
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// For ComboBoxes or quick display, e.g. “John Smith”.
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";
    }
}
