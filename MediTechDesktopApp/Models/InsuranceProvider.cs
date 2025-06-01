// File: Models\InsuranceProvider.cs
namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Maps to InsuranceProviders table: provider_id, name, contact_phone, contact_email
    /// </summary>
    public class InsuranceProvider
    {
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
    }
}
