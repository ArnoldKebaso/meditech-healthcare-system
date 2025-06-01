// File: Models\PatientInsurance.cs
using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Maps to PatientInsurance table:
    ///   patient_id, provider_id, policy_number, coverage_details, start_date, end_date
    /// </summary>
    public class PatientInsurance
    {
        public int PatientId { get; set; }
        public int ProviderId { get; set; }
        public string PolicyNumber { get; set; }
        public string CoverageDetails { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Composite (Patient / Provider / Policy)
        /// </summary>
        public string CompositeKey => $"Pt:{PatientId} / Prov:{ProviderId} / Pol:{PolicyNumber}";
    }
}
