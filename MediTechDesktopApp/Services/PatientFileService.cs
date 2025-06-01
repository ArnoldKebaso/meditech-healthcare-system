// File: Services\PatientFileService.cs
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// Service to CRUD PatientFiles (sp_GetPatientFiles, sp_AddPatientFile, sp_DeletePatientFile).
    /// </summary>
    public class PatientFileService
    {
        private readonly MySqlDbHelper _db;

        public PatientFileService()
        {
            _db = new MySqlDbHelper();
        }

        public List<PatientFile> GetAllFiles()
        {
            var table = _db.ExecuteStoredProcedure("sp_GetPatientFiles");
            var list = new List<PatientFile>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new PatientFile
                {
                    FileId = Convert.ToInt32(row["file_id"]),
                    PatientId = Convert.ToInt32(row["patient_id"]),
                    FileName = row["file_name"]?.ToString(),
                    FileType = row["file_type"]?.ToString(),
                    FileSizeBytes = row["file_size_bytes"] != DBNull.Value ? Convert.ToInt32(row["file_size_bytes"]) : 0,
                    UploadTimestamp = Convert.ToDateTime(row["upload_timestamp"])
                });
            }

            return list;
        }

        /// <summary>
        /// Save file to DB as LONGBLOB. We open the file and pass byte[] to sp.
        /// </summary>
        public void AddPatientFile(int patientId, string filenameWithPath)
        {
            if (string.IsNullOrWhiteSpace(filenameWithPath) || !File.Exists(filenameWithPath))
                throw new FileNotFoundException("File not found", filenameWithPath);

            byte[] data = File.ReadAllBytes(filenameWithPath);
            string fname = Path.GetFileName(filenameWithPath);
            string ftype = Path.GetExtension(filenameWithPath).TrimStart('.');

            var parameters = new Dictionary<string, object>
            {
                { "p_id",    patientId },
                { "f_name",  fname },
                { "f_type",  ftype },
                { "f_size",  data.Length },
                { "f_data",  data }
            };

            _db.ExecuteNonQuery("sp_AddPatientFile", parameters);
        }

        public void DeletePatientFile(int fileId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "f_id", fileId }
            };
            _db.ExecuteNonQuery("sp_DeletePatientFile", parameters);
        }
    }
}
