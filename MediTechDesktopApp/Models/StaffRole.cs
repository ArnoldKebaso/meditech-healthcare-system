// File: Models/StaffRole.cs
namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Lookup table: StaffRoles (role_id, name). 
    /// Each AdminStaff references a StaffRole.
    /// </summary>
    public class StaffRole
    {
        public int RoleId { get; set; }   // PK
        public string Name { get; set; }   // e.g. "Receptionist", "Billing Clerk"
    }
}
