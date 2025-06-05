namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Simple “Users” model matching the meditech.users table.
    /// </summary>
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}
