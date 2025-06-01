// File: Models\PatientFile.cs
using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Maps to PatientFiles table: file_id, patient_id, file_name, file_type, file_size_bytes, upload_timestamp, file_data
    /// </summary>
    public class PatientFile
    {
        public int FileId { get; set; }
        public int PatientId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public int FileSizeBytes { get; set; }
        public DateTime UploadTimestamp { get; set; }

        // We won’t bind the LONGBLOB directly in the grid; show size/filename only

        /// <summary>
        /// Display string for DataGrid
        /// </summary>
        public string DisplayInfo => $"{FileName} ({FileSizeBytes / 1024} KB)";
    }
}
