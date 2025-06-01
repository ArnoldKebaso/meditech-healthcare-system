// File: Services\PatientInsuranceService.cs
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// Service for PatientInsurance (sp_GetPatientInsurance, sp_AddPatientInsurance, etc.)
    /// </summary>
    public class PatientInsuranceService
    {
        private readonly MySqlDbHelper _db;

        public PatientInsuranceService()
        {
            _db = new MySqlDbHelper();
        }

        public List<PatientInsurance> GetAllPatientInsurance()
        {
            var table = _db.ExecuteStoredProcedure("sp_GetPatientInsurance");
            var list = new List<PatientInsurance>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new PatientInsurance
                {
                    PatientId = Convert.ToInt32(row["patient_id"]),
                    ProviderId = Convert.ToInt32(row["provider_id"]),
                    PolicyNumber = row["policy_number"]?.ToString(),
                    CoverageDetails = row["coverage_details"]?.ToString(),
                    StartDate = row["start_date"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["start_date"]) : null,
                    EndDate = row["end_date"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["end_date"]) : null
                });
            }

            return list;
        }

        public void AddPatientInsurance(PatientInsurance pi)
        {
            var parameters = new Dictionary<string, object>
            {
                { "p_id",      pi.PatientId },
                { "prov_id",   pi.ProviderId },
                { "pol_num",   pi.PolicyNumber },
                { "cov_details", pi.CoverageDetails },
                { "start_dt",  pi.StartDate },
                { "end_dt",    pi.EndDate }
            };

            _db.ExecuteNonQuery("sp_AddPatientInsurance", parameters);
        }

        public void UpdatePatientInsurance(PatientInsurance pi)
        {
            var parameters = new Dictionary<string, object>
            {
                { "p_id",      pi.PatientId },
                { "prov_id",   pi.ProviderId },
                { "pol_num",   pi.PolicyNumber },
                { "new_end",   pi.EndDate }
            };

            _db.ExecuteNonQuery("sp_UpdatePatientInsurance", parameters);
        }

        public void DeletePatientInsurance(PatientInsurance pi)
        {
            var parameters = new Dictionary<string, object>
            {
                { "p_id",     pi.PatientId },
                { "prov_id",  pi.ProviderId },
                { "pol_num",  pi.PolicyNumber }
            };

            _db.ExecuteNonQuery("sp_DeletePatientInsurance", parameters);
        }
    }
}
