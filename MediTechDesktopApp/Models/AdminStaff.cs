// File: Models\AdminStaff.cs
using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents an administrative staff member.
    /// </summary>
    public class AdminStaff
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
