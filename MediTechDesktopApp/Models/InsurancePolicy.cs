using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents one row in the InsurancePolicies table (joined to Providers).
    /// </summary>
    public class InsurancePolicy
    {
        public int PolicyId { get; set; }         // policy_id
        public int ProviderId { get; set; }       // provider_id
        public string ProviderName { get; set; }  // (joined column for display)
        public string PolicyNumber { get; set; }  // policy_number
        public string CoverageDetails { get; set; } // coverage_details
        public DateTime StartDate { get; set; }   // start_date
        public DateTime EndDate { get; set; }     // end_date
    }
}
