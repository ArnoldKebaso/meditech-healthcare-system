using System.Collections.ObjectModel;
using System.Linq;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// ViewModel to handle patient CRUD operations and expose an observable collection
    /// of Patient models for data binding in the UI.
    /// </summary>
    public class PatientViewModel
    {
        private readonly PatientService _service;

        /// <summary>
        /// Collection of all patients retrieved from the database.
        /// The UI (PatientView.xaml) binds its DataGrid’s ItemsSource to this property.
        /// </summary>
        public ObservableCollection<Patient> Patients { get; private set; } 
            = new ObservableCollection<Patient>();

        /// <summary>
        /// Holds the currently selected Patient (if the UI binds a SelectedItem).
        /// </summary>
        public Patient SelectedPatient { get; set; } = new Patient();

        /// <summary>
        /// Constructor: initializes the PatientService and immediately loads the patient list.
        /// </summary>
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

            // Retrieve all patients from the database via the service
            var allPatients = _service.GetAllPatients();
            foreach (var patient in allPatients)
            {
                Patients.Add(patient);
            }
        }

        /// <summary>
        /// Inserts a new patient into the database and reloads the collection.
        /// </summary>
        /// <param name="newPatient">The Patient object to add.</param>
        public void AddPatient(Patient newPatient)
        {
            if (newPatient == null)
                return;

            _service.AddPatient(newPatient);

            // Refresh the in‐memory list so the UI updates
            LoadPatients();
        }

        /// <summary>
        /// Convenience method to get a newline‐separated list of all patient full names.
        /// </summary>
        /// <returns>A single string containing each patient’s FullName on its own line.</returns>
        public string GetPatientNames()
        {
            return string.Join("\n", Patients.Select(p => p.FullName));
        }
    }
}
