// File: Models\Nurse.cs
using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Nurse model representing the Nurses table.
    /// </summary>
    public class Nurse
    {
        public int NurseId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
