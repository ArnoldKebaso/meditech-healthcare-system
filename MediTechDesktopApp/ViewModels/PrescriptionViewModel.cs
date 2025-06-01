using System.Collections.ObjectModel;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// ViewModel to handle Prescription CRUD and expose an ObservableCollection of Prescription.
    /// </summary>
    public class PrescriptionViewModel
    {
        private readonly PrescriptionService _service;

        /// <summary>
        /// Collection bound to PrescriptionView’s DataGrid.
        /// </summary>
        public ObservableCollection<Prescription> Prescriptions { get; } = new ObservableCollection<Prescription>();

        public Prescription SelectedPrescription { get; set; } = new Prescription();

        public PrescriptionViewModel()
        {
            _service = new PrescriptionService();
            LoadPrescriptions();
        }

        /// <summary>
        /// Reloads all prescriptions from the DB.
        /// </summary>
        public void LoadPrescriptions()
        {
            Prescriptions.Clear();
            var all = _service.GetAllPrescriptions();
            foreach (var p in all)
            {
                Prescriptions.Add(p);
            }
        }

        /// <summary>
        /// Inserts a new prescription via the Service and reloads list.
        /// </summary>
        public void AddPrescription(Prescription newPres)
        {
            if (newPres == null) return;
            _service.AddPrescription(newPres);
            LoadPrescriptions();
        }

        /// <summary>
        /// Deletes the specified prescription.
        /// </summary>
        public void DeletePrescription(int prescId)
        {
            _service.DeletePrescription(prescId);
            LoadPrescriptions();
        }
    }
}
