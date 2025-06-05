// File: ViewModels\PrescriptionViewModel.cs
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// ViewModel exposing Prescription list and CRUD for PrescriptionView.xaml.
    /// </summary>
    public class PrescriptionViewModel
    {
        private readonly PrescriptionService _service;

        public ObservableCollection<Prescription> Prescriptions { get; private set; }
            = new ObservableCollection<Prescription>();

        public Prescription SelectedPrescription { get; set; } = new Prescription();

        public PrescriptionViewModel()
        {
            _service = new PrescriptionService();
            LoadPrescriptions();
        }

        /// <summary>
        /// Loads all prescriptions from DB.
        /// </summary>
        public void LoadPrescriptions()
        {
            Prescriptions.Clear();
            var list = _service.GetAllPrescriptions();
            foreach (var p in list)
            {
                Prescriptions.Add(p);
            }
        }

        /// <summary>
        /// Adds a new prescription to DB, then reloads.
        /// </summary>
        public void AddPrescription(Prescription p)
        {
            if (p == null) return;
            _service.AddPrescription(p);
            LoadPrescriptions();
        }

        /// <summary>
        /// Updates an existing prescription, then reloads.
        /// </summary>
        public void UpdatePrescription(Prescription p)
        {
            if (p == null) return;
            _service.UpdatePrescription(p);
            LoadPrescriptions();
        }

        /// <summary>
        /// Deletes prescription by ID, then reloads.
        /// </summary>
        public void DeletePrescription(int prescriptionId)
        {
            _service.DeletePrescription(prescriptionId);
            LoadPrescriptions();
        }

        /// <summary>
        /// Returns newline list of prescription IDs (for quick debugging).
        /// </summary>
        public string GetPrescriptionIds()
        {
            return string.Join("\n", Prescriptions.Select(p => p.PrescriptionId));
        }
    }
}
