using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;      // ← Reference System.Configuration.dll is required
using MySql.Data.MySqlClient;

namespace MediTechDesktopApp.DataAccess
{
    /// <summary>
    /// A simple ADO.NET helper for MySQL that wraps:
    ///   • ExecuteRawQuery(...)    → returns a DataTable from any SELECT
    ///   • ExecuteNonQuery(...)    → executes any INSERT/UPDATE/DELETE
    ///
    /// It reads its connection string from app.config under <connectionStrings name="Default">.
    /// </summary>
    public class MySqlDbHelper
    {
        private readonly string _connectionString;

        /// <summary>
        /// Constructor: Reads connection string named "Default" from app.config.
        /// </summary>
        public MySqlDbHelper()
        {
            var csSettings = ConfigurationManager.ConnectionStrings["Default"];
            if (csSettings == null || string.IsNullOrWhiteSpace(csSettings.ConnectionString))
            {
                throw new InvalidOperationException(
                    "Could not find a valid connection string named 'Default' in app.config.");
            }
            _connectionString = csSettings.ConnectionString;
        }
        public DataRow ExecuteSingleRow(string sql, Dictionary<string, object> parms)
        {
            DataTable dt = ExecuteRawQuery(sql, parms);
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }


        /// <summary>
        /// Executes any SELECT statement (CommandType.Text) and returns a DataTable.
        /// </summary>
        /// <param name="sqlQuery">A valid SELECT SQL statement.</param>
        /// <param name="parameters">
        ///   Optional dictionary of parameterName→value (no "@" prefix in keys; 
        ///   e.g. { "first_name", "John" } becomes cmd.Parameters.AddWithValue("@first_name", "John")).
        /// </param>
        public DataTable ExecuteRawQuery(string sqlQuery, Dictionary<string, object> parameters = null)
        {
            using (var conn = new MySqlConnection(_connectionString))
            using (var cmd = new MySqlCommand(sqlQuery, conn))
            {
                cmd.CommandType = CommandType.Text;

                if (parameters != null)
                {
                    foreach (var kvp in parameters)
                        cmd.Parameters.AddWithValue($"@{kvp.Key}", kvp.Value ?? DBNull.Value);
                }

                var adapter = new MySqlDataAdapter(cmd);
                var dtResult = new DataTable();

                conn.Open();
                adapter.Fill(dtResult);
                return dtResult;
            }
        }

        /// <summary>
        /// Executes any INSERT / UPDATE / DELETE (CommandType.Text) and returns the row count.
        /// </summary>
        /// <param name="sqlCommandText">A valid INSERT/UPDATE/DELETE SQL statement with "@param" placeholders.</param>
        /// <param name="parameters">
        ///   Dictionary of parameterName→value (no "@" prefix in keys). 
        ///   Example: { "first_name", "John" } → cmd.Parameters.AddWithValue("@first_name", "John").
        /// </param>
        public int ExecuteNonQuery(string sqlCommandText, Dictionary<string, object> parameters = null)
        {
            using (var conn = new MySqlConnection(_connectionString))
            using (var cmd = new MySqlCommand(sqlCommandText, conn))
            {
                cmd.CommandType = CommandType.Text;

                if (parameters != null)
                {
                    foreach (var kvp in parameters)
                        cmd.Parameters.AddWithValue($"@{kvp.Key}", kvp.Value ?? DBNull.Value);
                }

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
