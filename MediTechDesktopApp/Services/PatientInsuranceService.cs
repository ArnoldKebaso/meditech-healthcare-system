using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// CRUD for PatientInsurance table, plus lookup lists for ComboBoxes.
    /// </summary>
    public class PatientInsuranceService
    {
        private readonly MySqlDbHelper _db;

        public PatientInsuranceService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Returns all PatientInsurance rows joined to Patients and InsurancePolicies so we can display full names.
        /// NOTE: coverage_amount is used instead of coverage_details.
        /// </summary>
        public List<PatientInsurance> GetAllPatientInsurance()
        {
            var results = new List<PatientInsurance>();

            string sql = @"
                SELECT 
                  pi.patient_id,
                  pi.policy_id,
                  CONCAT(p.first_name, ' ', p.last_name) AS PatientFullName,
                  ip.policy_number        AS PolicyNumber,
                  ip.coverage_details       AS CoverageDetails,
                  ip.start_date           AS StartDate,
                  ip.end_date             AS EndDate
                FROM PatientInsurance pi
                JOIN Patients          p  ON pi.patient_id = p.patient_id
                JOIN InsurancePolicies ip ON pi.policy_id  = ip.policy_id
                ORDER BY p.last_name, p.first_name;
            ";

            DataTable dt = _db.ExecuteRawQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                results.Add(new PatientInsurance
                {
                    PatientId = Convert.ToInt32(row["patient_id"]),
                    PolicyId = Convert.ToInt32(row["policy_id"]),
                    PatientFullName = row["PatientFullName"].ToString(),
                    PolicyNumber = row["PolicyNumber"].ToString(),
                    CoverageDetails = row["CoverageDetails"].ToString(),
                    StartDate = Convert.ToDateTime(row["StartDate"]),
                    EndDate = Convert.ToDateTime(row["EndDate"])
                });
            }

            return results;
        }

        /// <summary>
        /// Inserts a new row into PatientInsurance.
        /// </summary>
        public void AddPatientInsurance(PatientInsurance pi)
        {
            string sql = @"
                INSERT INTO PatientInsurance
                  (patient_id, policy_id)
                VALUES
                  (@patient_id, @policy_id);
            ";
            var parms = new Dictionary<string, object>
            {
                { "patient_id", pi.PatientId },
                { "policy_id",  pi.PolicyId }
            };
            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Deletes an existing PatientInsurance by composite key.
        /// </summary>
        public void DeletePatientInsurance(int patientId, int policyId)
        {
            string sql = @"
                DELETE FROM PatientInsurance
                 WHERE patient_id = @patient_id
                   AND policy_id  = @policy_id;
            ";
            var parms = new Dictionary<string, object>
            {
                { "patient_id", patientId },
                { "policy_id",  policyId }
            };
            _db.ExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// Returns a list of ComboItem(Id, FullName) for populating the Patient ComboBox.
        /// </summary>
        public List<ComboItem> GetAllPatientsForCombo()
        {
            string sql = @"
                SELECT 
                  patient_id, 
                  CONCAT(first_name, ' ', last_name) AS FullName
                FROM Patients
                ORDER BY last_name, first_name;
            ";
            var results = new List<ComboItem>();
            DataTable dt = _db.ExecuteRawQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                results.Add(new ComboItem
                {
                    Id = Convert.ToInt32(row["patient_id"]),
                    Name = row["FullName"].ToString()
                });
            }

            return results;
        }

        /// <summary>
        /// Returns a list of ComboItem(Id, PolicyNumber) for populating the Policy ComboBox.
        /// </summary>
        public List<ComboItem> GetAllPoliciesForCombo()
        {
            string sql = @"
                SELECT 
                  policy_id, 
                  policy_number 
                FROM InsurancePolicies
                ORDER BY policy_number;
            ";
            var results = new List<ComboItem>();
            DataTable dt = _db.ExecuteRawQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                results.Add(new ComboItem
                {
                    Id = Convert.ToInt32(row["policy_id"]),
                    Name = row["policy_number"].ToString()
                });
            }

            return results;
        }
    }
}
