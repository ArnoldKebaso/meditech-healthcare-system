using System.Collections.ObjectModel;
using System.Linq;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// ViewModel for PatientView.xaml.
    /// Exposes an ObservableCollection of Patient models for data binding in the UI.
    /// Also handles Add, Update, Delete, and Refresh logic.
    /// </summary>
    public class PatientViewModel
    {
        private readonly PatientService _service;

        /// <summary>
        /// Collection of all patients retrieved from the database.
        /// The UI binds its DataGrid’s ItemsSource to this property.
        /// </summary>
        public ObservableCollection<Patient> Patients { get; private set; }
            = new ObservableCollection<Patient>();

        /// <summary>
        /// Holds the currently selected Patient (if the UI binds a SelectedItem).
        /// </summary>
        public Patient SelectedPatient { get; set; } = new Patient();

        public PatientViewModel()
        {
            _service = new PatientService();
            LoadPatients();
        }

        /// <summary>
        /// Clears the existing collection and repopulates it by calling the service layer.
        /// </summary>
        public void LoadPatients()
        {
            Patients.Clear();
            var allPatients = _service.GetAllPatients();
            foreach (var patient in allPatients)
            {
                Patients.Add(patient);
            }
        }

        /// <summary>
        /// Inserts a new patient into the database and reloads the collection.
        /// </summary>
        public void AddPatient(Patient newPatient)
        {
            if (newPatient == null)
                return;

            _service.AddPatient(newPatient);
            LoadPatients();
        }

        /// <summary>
        /// Updates an existing patient in the database and reloads the collection.
        /// </summary>
        public void UpdatePatient(Patient updatedPatient)
        {
            if (updatedPatient == null || updatedPatient.PatientId == 0)
                return;

            _service.UpdatePatient(updatedPatient);
            LoadPatients();
        }

        /// <summary>
        /// Deletes the selected patient from the database and reloads the collection.
        /// </summary>
        public void DeletePatient(Patient patientToDelete)
        {
            if (patientToDelete == null || patientToDelete.PatientId == 0)
                return;

            _service.DeletePatient(patientToDelete.PatientId);
            LoadPatients();
        }

        /// <summary>
        /// Provides a newline‐separated list of patient full names.
        /// </summary>
        public string GetPatientNames()
        {
            return string.Join("\n", Patients.Select(p => p.FullName));
        }
    }
}
