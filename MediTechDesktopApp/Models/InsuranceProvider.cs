namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents one row in the InsuranceProviders table.
    /// </summary>
    public class InsuranceProvider
    {
        public int ProviderId { get; set; }    // provider_id
        public string Name { get; set; }       // name
        public string ContactPhone { get; set; } // contact_phone
        public string ContactEmail { get; set; } // contact_email
    }
}
