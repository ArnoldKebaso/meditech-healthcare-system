// File: DataAccess\MySqlDbHelper.cs

using System;
using System.Collections.Generic;
using System.Configuration;       // <— System.Configuration.ConfigurationManager
using System.Data;
using MySql.Data.MySqlClient;      // You must have the MySql.Data NuGet/package reference

namespace MediTechDesktopApp.DataAccess
{
    /// <summary>
    /// Helper class to manage MySQL database connectivity and execute stored procedures.
    /// </summary>
    public class MySqlDbHelper
    {
        // Reads the connection string from <connectionStrings> in App.config
        private readonly string _connectionString;

        public MySqlDbHelper()
        {
            // This will read the <add name="Default" ...> entry from App.config
            var connStrSettings = ConfigurationManager.ConnectionStrings["Default"];
            if (connStrSettings == null || string.IsNullOrWhiteSpace(connStrSettings.ConnectionString))
            {
                throw new ConfigurationErrorsException(
                    "Missing or empty <connectionStrings> entry named 'Default' in App.config."
                );
            }

            _connectionString = connStrSettings.ConnectionString;
        }

        /// <summary>
        /// Executes a stored procedure (SELECT) and returns the results as a DataTable.
        /// </summary>
        public DataTable ExecuteStoredProcedure(string procedureName, Dictionary<string, object> parameters = null)
        {
            using (var conn = new MySqlConnection(_connectionString))
            using (var cmd = new MySqlCommand(procedureName, conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        // param.Key must match the stored-proc parameter name (e.g. "p_first")
                        cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                var dt = new DataTable();
                var adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }
        }

        /// <summary>
        /// Executes a stored procedure (INSERT/UPDATE/DELETE) and returns affected-rows count.
        /// </summary>
        public int ExecuteNonQuery(string procedureName, Dictionary<string, object> parameters = null)
        {
            using (var conn = new MySqlConnection(_connectionString))
            using (var cmd = new MySqlCommand(procedureName, conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
