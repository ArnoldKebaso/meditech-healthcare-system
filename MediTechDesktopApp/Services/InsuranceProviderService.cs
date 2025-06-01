// File: Services\InsuranceProviderService.cs
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// Service to CRUD InsuranceProviders (sp_GetProviders, sp_AddProvider, etc.)
    /// </summary>
    public class InsuranceProviderService
    {
        private readonly MySqlDbHelper _db;

        public InsuranceProviderService()
        {
            _db = new MySqlDbHelper();
        }

        public List<InsuranceProvider> GetAllProviders()
        {
            var table = _db.ExecuteStoredProcedure("sp_GetProviders");
            var list = new List<InsuranceProvider>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new InsuranceProvider
                {
                    ProviderId = Convert.ToInt32(row["provider_id"]),
                    Name = row["name"]?.ToString(),
                    ContactPhone = row["contact_phone"]?.ToString(),
                    ContactEmail = row["contact_email"]?.ToString()
                });
            }

            return list;
        }

        public void AddProvider(InsuranceProvider prov)
        {
            var parameters = new Dictionary<string, object>
            {
                { "prov_name",  prov.Name },
                { "prov_phone", prov.ContactPhone },
                { "prov_email", prov.ContactEmail }
            };
            _db.ExecuteNonQuery("sp_AddProvider", parameters);
        }

        public void UpdateProvider(int provId, string phone, string email)
        {
            var parameters = new Dictionary<string, object>
            {
                { "prov_id",   provId },
                { "prov_phone", phone },
                { "prov_email", email }
            };
            _db.ExecuteNonQuery("sp_UpdateProvider", parameters);
        }

        public void DeleteProvider(int provId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "prov_id", provId }
            };
            _db.ExecuteNonQuery("sp_DeleteProvider", parameters);
        }
    }
}
