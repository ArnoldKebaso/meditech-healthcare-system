using System.Collections.ObjectModel;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// ViewModel to handle MedicalRecord CRUD and expose an ObservableCollection of MedicalRecord.
    /// </summary>
    public class MedicalRecordViewModel
    {
        private readonly MedicalRecordService _service;

        /// <summary>
        /// Collection bound to MedicalRecordView’s DataGrid.
        /// </summary>
        public ObservableCollection<MedicalRecord> Records { get; } = new ObservableCollection<MedicalRecord>();

        public MedicalRecord SelectedRecord { get; set; } = new MedicalRecord();

        public MedicalRecordViewModel()
        {
            _service = new MedicalRecordService();
            LoadRecords();
        }

        /// <summary>
        /// Reloads all medical records from the DB.
        /// </summary>
        public void LoadRecords()
        {
            Records.Clear();
            var all = _service.GetAllMedicalRecords();
            foreach (var r in all)
            {
                Records.Add(r);
            }
        }

        /// <summary>
        /// Inserts a new medical record via the Service and reloads list.
        /// </summary>
        public void AddRecord(MedicalRecord newRecord)
        {
            if (newRecord == null) return;
            _service.AddMedicalRecord(newRecord);
            LoadRecords();
        }

        /// <summary>
        /// Deletes the specified record.
        /// </summary>
        public void DeleteRecord(int recordId)
        {
            _service.DeleteMedicalRecord(recordId);
            LoadRecords();
        }
    }
}
