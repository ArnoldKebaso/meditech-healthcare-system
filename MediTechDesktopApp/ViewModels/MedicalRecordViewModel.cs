// File: ViewModels\MedicalRecordViewModel.cs
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// ViewModel exposing MedicalRecord list and CRUD to MedicalRecordView.xaml.
    /// </summary>
    public class MedicalRecordViewModel
    {
        private readonly MedicalRecordService _service;

        public ObservableCollection<MedicalRecord> Records { get; private set; }
            = new ObservableCollection<MedicalRecord>();

        public MedicalRecord SelectedRecord { get; set; } = new MedicalRecord();

        public MedicalRecordViewModel()
        {
            _service = new MedicalRecordService();
            LoadRecords();
        }

        /// <summary>
        /// Loads all medical records from DB.
        /// </summary>
        public void LoadRecords()
        {
            Records.Clear();
            var list = _service.GetAllRecords();
            foreach (var rec in list)
            {
                Records.Add(rec);
            }
        }

        /// <summary>
        /// Adds a new medical record, then reloads.
        /// </summary>
        public void AddRecord(MedicalRecord rec)
        {
            if (rec == null) return;
            _service.AddMedicalRecord(rec);
            LoadRecords();
        }

        /// <summary>
        /// Updates an existing record’s text fields.
        /// </summary>
        public void UpdateRecord(MedicalRecord rec)
        {
            if (rec == null) return;
            _service.UpdateMedicalRecord(rec);
            LoadRecords();
        }

        /// <summary>
        /// Deletes a record by ID, then reloads.
        /// </summary>
        public void DeleteRecord(int recordId)
        {
            _service.DeleteMedicalRecord(recordId);
            LoadRecords();
        }

        /// <summary>
        /// Returns newline list of record IDs (for quick debugging).
        /// </summary>
        public string GetRecordIds()
        {
            return string.Join("\n", Records.Select(r => r.RecordId));
        }
    }
}
