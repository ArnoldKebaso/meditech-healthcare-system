using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// CRUD for InsurancePolicies (via stored procedures).
    /// </summary>
    public class InsurancePolicyService
    {
        private readonly MySqlDbHelper _db;

        public InsurancePolicyService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Fetch all policies along with their provider name.
        /// </summary>
        public List<InsurancePolicy> GetAllPolicies()
        {
            var results = new List<InsurancePolicy>();

            string sql = "CALL sp_GetInsurancePolicies();";
            DataTable dt = _db.ExecuteRawQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                results.Add(new InsurancePolicy
                {
                    PolicyId = Convert.ToInt32(row["policy_id"]),
                    ProviderId = Convert.ToInt32(row["provider_id"]),
                    ProviderName = row["provider_name"].ToString(),
                    PolicyNumber = row["policy_number"].ToString(),
                    CoverageDetails = row["coverage_details"].ToString(),
                    StartDate = Convert.ToDateTime(row["start_date"]),
                    EndDate = Convert.ToDateTime(row["end_date"])
                });
            }

            return results;
        }

        /// <summary>
        /// Inserts new policy via sp_AddInsurancePolicy.
        /// </summary>
        public void AddPolicy(InsurancePolicy pol)
        {
            // sp_AddInsurancePolicy(
            //   IN ip_prov_id INT,
            //   IN ip_policy_num VARCHAR(100),
            //   IN ip_cov_details TEXT,
            //   IN ip_start_date DATE,
            //   IN ip_end_date DATE
            // )
            string sql = @"
                CALL sp_AddInsurancePolicy(
                    @ip_prov_id, 
                    @ip_policy_num, 
                    @ip_cov_details, 
                    @ip_start_date, 
                    @ip_end_date);
            ";
            var parameters = new Dictionary<string, object>
            {
                { "ip_prov_id",     pol.ProviderId },
                { "ip_policy_num",  pol.PolicyNumber },
                { "ip_cov_details", pol.CoverageDetails },
                { "ip_start_date",  pol.StartDate },
                { "ip_end_date",    pol.EndDate }
            };
            _db.ExecuteRawQuery(sql, parameters);
        }

        /// <summary>
        /// Updates an existing policy via sp_UpdateInsurancePolicy.
        /// </summary>
        public void UpdatePolicy(InsurancePolicy pol)
        {
            // sp_UpdateInsurancePolicy(
            //   IN ip_id INT,
            //   IN ip_policy_num VARCHAR(100),
            //   IN ip_cov_details TEXT,
            //   IN ip_start_date DATE,
            //   IN ip_end_date DATE
            // )
            string sql = @"
                CALL sp_UpdateInsurancePolicy(
                    @ip_id, 
                    @ip_policy_num, 
                    @ip_cov_details, 
                    @ip_start_date, 
                    @ip_end_date);
            ";
            var parameters = new Dictionary<string, object>
            {
                { "ip_id",           pol.PolicyId },
                { "ip_policy_num",   pol.PolicyNumber },
                { "ip_cov_details",  pol.CoverageDetails },
                { "ip_start_date",   pol.StartDate },
                { "ip_end_date",     pol.EndDate }
            };
            _db.ExecuteRawQuery(sql, parameters);
        }

        /// <summary>
        /// Deletes a policy via sp_DeleteInsurancePolicy.
        /// </summary>
        public void DeletePolicy(int policyId)
        {
            string sql = "CALL sp_DeleteInsurancePolicy(@ip_id);";
            var parameters = new Dictionary<string, object>
            {
                { "ip_id", policyId }
            };
            _db.ExecuteRawQuery(sql, parameters);
        }

        /// <summary>
        /// Helper: load list of all providers (id + name) to populate provider ComboBox.
        /// (We reuse InsuranceProviderService to get that.)
        /// </summary>
        public List<(int id, string name)> GetAllProvidersForCombo()
        {
            // We want just (provider_id, name) so that user picks a provider first.
            var list = new List<(int, string)>();
            string sql = @"
                SELECT provider_id, name
                  FROM InsuranceProviders
                 ORDER BY name;
            ";
            DataTable dt = _db.ExecuteRawQuery(sql);
            foreach (DataRow row in dt.Rows)
            {
                list.Add((
                    Convert.ToInt32(row["provider_id"]),
                    row["name"].ToString()
                ));
            }
            return list;
        }
    }
}
