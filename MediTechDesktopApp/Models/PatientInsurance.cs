using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents one PatientInsurance row, 
    /// both for inserting (patient_id, policy_id) 
    /// and for reading (joined columns).
    /// </summary>
    public class PatientInsurance
    {
        // Primary‐key columns (composite PK in patientinsurance table)
        public int PatientId { get; set; }
        public int PolicyId { get; set; }

        // Joined/display columns (read‐only)
        public string PatientFullName { get; set; }    // alias: PatientFullName
        public string PolicyNumber { get; set; }    // alias: PolicyNumber
        public string CoverageDetails { get; set; }    // alias: CoverageDetails
        public DateTime StartDate { get; set; }    // alias: StartDate  (from insurancepolicies)
        public DateTime EndDate { get; set; }    // alias: EndDate    (from insurancepolicies)
    }
}
