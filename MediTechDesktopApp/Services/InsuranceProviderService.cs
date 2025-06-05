using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// CRUD for InsuranceProviders (via stored procedures).
    /// </summary>
    public class InsuranceProviderService
    {
        private readonly MySqlDbHelper _db;

        public InsuranceProviderService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Fetches all providers via sp_GetProviders.
        /// </summary>
        public List<InsuranceProvider> GetAllProviders()
        {
            var results = new List<InsuranceProvider>();

            string sql = "CALL sp_GetProviders();";
            DataTable dt = _db.ExecuteRawQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                results.Add(new InsuranceProvider
                {
                    ProviderId = Convert.ToInt32(row["provider_id"]),
                    Name = row["name"].ToString(),
                    ContactPhone = row["contact_phone"].ToString(),
                    ContactEmail = row["contact_email"].ToString()
                });
            }

            return results;
        }

        /// <summary>
        /// Inserts new provider via sp_AddProvider.
        /// </summary>
        public void AddProvider(InsuranceProvider prov)
        {
            // sp_AddProvider(IN prov_name VARCHAR(150), IN prov_phone VARCHAR(20), IN prov_email VARCHAR(150))
            string sql = @"
                CALL sp_AddProvider(@prov_name, @prov_phone, @prov_email);
            ";
            var parameters = new Dictionary<string, object>
            {
                { "prov_name",  prov.Name },
                { "prov_phone", prov.ContactPhone },
                { "prov_email", prov.ContactEmail }
            };
            _db.ExecuteRawQuery(sql, parameters);
        }

        /// <summary>
        /// Updates existing provider via sp_UpdateProvider.
        /// </summary>
        public void UpdateProvider(InsuranceProvider prov)
        {
            // sp_UpdateProvider(IN prov_id INT, IN prov_phone VARCHAR(20), IN prov_email VARCHAR(150))
            string sql = @"
                CALL sp_UpdateProvider(@prov_id, @prov_phone, @prov_email);
            ";
            var parameters = new Dictionary<string, object>
            {
                { "prov_id",    prov.ProviderId },
                { "prov_phone", prov.ContactPhone },
                { "prov_email", prov.ContactEmail }
            };
            _db.ExecuteRawQuery(sql, parameters);
        }

        /// <summary>
        /// Deletes a provider by ID via sp_DeleteProvider.
        /// </summary>
        public void DeleteProvider(int providerId)
        {
            string sql = "CALL sp_DeleteProvider(@prov_id);";
            var parameters = new Dictionary<string, object>
            {
                { "prov_id", providerId }
            };
            _db.ExecuteRawQuery(sql, parameters);
        }
    }
}
