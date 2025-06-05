using System;

namespace MediTechDesktopApp.Models
{
    /// <summary>
    /// Represents one row in the PatientFiles table.
    /// </summary>
    public class PatientFile
    {
        public int FileId { get; set; }           // corresponds to file_id
        public int PatientId { get; set; }        // corresponds to patient_id
        public string FileName { get; set; }      // file_name
        public string FileType { get; set; }      // file_type
        public int FileSizeBytes { get; set; }    // file_size_bytes
        public DateTime UploadTimestamp { get; set; } // upload_timestamp
        public byte[] FileData { get; set; }      // file_data

        /// <summary>
        /// A convenience property to display the patient's full name.
        /// This is filled via a JOIN in the service.
        /// </summary>
        public string PatientFullName { get; set; }
    }
}
