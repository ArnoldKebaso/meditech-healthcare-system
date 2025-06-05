using System;
using System.Collections.ObjectModel;
using System.Linq;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// ViewModel for DoctorView.xaml.
    /// Exposes an ObservableCollection of Doctor models for data binding in the UI.
    /// Handles Add, Update, Delete, and Refresh logic.
    /// </summary>
    public class DoctorViewModel
    {
        private readonly DoctorService _service;

        /// <summary>
        /// Collection of all doctors loaded from the database.
        /// The UI binds its DataGrid’s ItemsSource to this property.
        /// </summary>
        public ObservableCollection<Doctor> Doctors { get; private set; }
            = new ObservableCollection<Doctor>();

        /// <summary>
        /// Holds the currently selected doctor (if the UI binds a SelectedItem).
        /// </summary>
        public Doctor SelectedDoctor { get; set; } = new Doctor();

        public DoctorViewModel()
        {
            _service = new DoctorService();
            LoadDoctors();
        }

        /// <summary>
        /// Loads all doctors from the service and refreshes the ObservableCollection.
        /// </summary>
        public void LoadDoctors()
        {
            Doctors.Clear();
            var allDocs = _service.GetAllDoctors();
            foreach (var doc in allDocs)
            {
                Doctors.Add(doc);
            }
        }

        /// <summary>
        /// Adds a new doctor via the service and refreshes the list.
        /// </summary>
        public void AddDoctor(Doctor newDoc)
        {
            if (newDoc == null)
                return;

            _service.AddDoctor(newDoc);
            LoadDoctors();
        }

        /// <summary>
        /// Updates an existing doctor via the service and refreshes the list.
        /// </summary>
        public void UpdateDoctor(Doctor doc)
        {
            if (doc == null || doc.DoctorId == 0)
                return;

            _service.UpdateDoctor(doc);
            LoadDoctors();
        }

        /// <summary>
        /// Deletes the selected doctor via the service and refreshes the list.
        /// </summary>
        public void DeleteDoctor(Doctor doc)
        {
            if (doc == null || doc.DoctorId == 0)
                return;

            _service.DeleteDoctor(doc.DoctorId);
            LoadDoctors();
        }

        /// <summary>
        /// Returns a newline‐separated list of all doctor full names.
        /// </summary>
        public string GetDoctorNames()
        {
            return string.Join("\n", Doctors.Select(d => d.FullName));
        }
    }
}
