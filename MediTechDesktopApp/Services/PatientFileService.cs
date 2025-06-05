using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// Encapsulates all database operations for PatientFiles.
    /// </summary>
    public class PatientFileService
    {
        private readonly MySqlDbHelper _db;

        public PatientFileService()
        {
            _db = new MySqlDbHelper();
        }

        /// <summary>
        /// Fetches all patient files, joined with Patients to get the PatientFullName.
        /// </summary>
        public List<PatientFile> GetAllPatientFiles()
        {
            var results = new List<PatientFile>();

            string sql = @"
                SELECT 
                  pf.file_id,
                  pf.patient_id,
                  pf.file_name,
                  pf.file_type,
                  pf.file_size_bytes,
                  pf.upload_timestamp,
                  -- We do NOT fetch the actual file_data in the listing (keeps grid fast):
                  CONCAT(p.first_name, ' ', p.last_name) AS PatientFullName
                FROM PatientFiles pf
                JOIN Patients p ON pf.patient_id = p.patient_id
                ORDER BY pf.upload_timestamp DESC;
            ";

            DataTable dt = _db.ExecuteRawQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                results.Add(new PatientFile
                {
                    FileId = Convert.ToInt32(row["file_id"]),
                    PatientId = Convert.ToInt32(row["patient_id"]),
                    FileName = row["file_name"].ToString(),
                    FileType = row["file_type"].ToString(),
                    FileSizeBytes = Convert.ToInt32(row["file_size_bytes"]),
                    UploadTimestamp = Convert.ToDateTime(row["upload_timestamp"]),
                    PatientFullName = row["PatientFullName"].ToString(),
                    // Note: we did not select file_data here—just metadata for listing.
                });
            }

            return results;
        }

        /// <summary>
        /// Uploads (inserts) a new PatientFile (including actual blob) into the database.
        /// </summary>
        public void AddPatientFile(PatientFile pf)
        {
            // The stored procedure (sp_AddPatientFile) expects:
            //   IN p_id INT, IN f_name VARCHAR(255), IN f_type VARCHAR(100), IN f_size INT, IN f_data LONGBLOB
            // We'll call it as a raw command:
            string sql = @"
                CALL sp_AddPatientFile(@patient_id, @file_name, @file_type, @file_size_bytes, @file_data);
            ";

            var parameters = new Dictionary<string, object>
            {
                { "patient_id",     pf.PatientId },
                { "file_name",      pf.FileName },
                { "file_type",      pf.FileType },
                { "file_size_bytes",pf.FileSizeBytes },
                { "file_data",      pf.FileData }
            };

            _db.ExecuteRawQuery(sql, parameters);
        }

        /// <summary>
        /// Deletes a PatientFile by file_id.
        /// </summary>
        public void DeletePatientFile(int fileId)
        {
            // The stored proc: sp_DeletePatientFile(IN f_id INT)
            string sql = "CALL sp_DeletePatientFile(@file_id);";
            var parameters = new Dictionary<string, object>
            {
                { "file_id", fileId }
            };
            _db.ExecuteRawQuery(sql, parameters);
        }

        /// <summary>
        /// Fetches the BLOB data (byte[]) for a given file_id, so we can save to disk.
        /// </summary>
        public byte[] GetFileDataById(int fileId)
        {
            string sql = @"
                SELECT file_data 
                  FROM PatientFiles 
                 WHERE file_id = @file_id;
            ";
            var parameters = new Dictionary<string, object>
            {
                { "file_id", fileId }
            };

            DataTable dt = _db.ExecuteRawQuery(sql, parameters);
            if (dt.Rows.Count == 0)
                return null;

            return dt.Rows[0]["file_data"] as byte[];
        }

        /// <summary>
        /// Helper: Returns a list of all patients (id + full name) to populate the ComboBox.
        /// </summary>
        public List<(int id, string fullName)> GetAllPatientsForCombo()
        {
            string sql = @"
                SELECT patient_id, CONCAT(first_name, ' ', last_name) AS FullName
                  FROM Patients
                 ORDER BY last_name, first_name;
            ";
            var results = new List<(int, string)>();
            DataTable dt = _db.ExecuteRawQuery(sql);

            foreach (DataRow row in dt.Rows)
            {
                results.Add((
                    Convert.ToInt32(row["patient_id"]),
                    row["FullName"].ToString()
                ));
            }

            return results;
        }

        /// <summary>
        /// *** Previously unimplemented.  Now returns actual Patient objects by delegating
        ///     to your existing PatientService.GetAllPatients() method. ***
        /// </summary>
        public List<Patient> GetAllPatients()
        {
            // Simply call your existing PatientService to fetch the entire Patient list.
            var patientService = new PatientService();
            return patientService.GetAllPatients();
        }
    }
}
