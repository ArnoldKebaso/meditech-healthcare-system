// File: ViewModels\DoctorViewModel.cs
using System.Collections.ObjectModel;
using System.Linq;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// ViewModel to handle doctor CRUD operations and expose an observable collection
    /// of Doctor models for data binding in the UI.
    /// </summary>
    public class DoctorViewModel
    {
        private readonly DoctorService _service;

        /// <summary>
        /// Collection of all doctors retrieved from the database.
        /// The UI (DoctorView.xaml) binds its DataGrid’s ItemsSource to this property.
        /// </summary>
        public ObservableCollection<Doctor> Doctors { get; private set; }
            = new ObservableCollection<Doctor>();

        /// <summary>
        /// Holds the currently selected Doctor (if the UI binds a SelectedItem).
        /// </summary>
        public Doctor SelectedDoctor { get; set; } = new Doctor();

        /// <summary>
        /// Constructor: initializes the DoctorService and immediately loads the doctor list.
        /// </summary>
        public DoctorViewModel()
        {
            _service = new DoctorService();
            LoadDoctors();
        }

        /// <summary>
        /// Clears the existing collection and repopulates it by calling the service layer.
        /// </summary>
        public void LoadDoctors()
        {
            Doctors.Clear();
            var allDoctors = _service.GetAllDoctors();
            foreach (var doc in allDoctors)
            {
                Doctors.Add(doc);
            }
        }

        /// <summary>
        /// Inserts a new doctor into the database and reloads the collection.
        /// </summary>
        public void AddDoctor(Doctor newDoctor)
        {
            if (newDoctor == null)
                return;

            _service.AddDoctor(newDoctor);
            LoadDoctors();
        }

        /// <summary>
        /// Updates the selected doctor in the database and reloads the collection.
        /// </summary>
        public void UpdateDoctor(Doctor doctorToUpdate)
        {
            if (doctorToUpdate == null || doctorToUpdate.DoctorId == 0)
                return;

            _service.UpdateDoctor(doctorToUpdate);
            LoadDoctors();
        }

        /// <summary>
        /// Deletes the selected doctor from the database and reloads the collection.
        /// </summary>
        public void DeleteDoctor(int doctorId)
        {
            if (doctorId == 0)
                return;

            _service.DeleteDoctor(doctorId);
            LoadDoctors();
        }

        /// <summary>
        /// Convenience method to get a newline‐separated list of all doctor full names.
        /// </summary>
        public string GetDoctorNames()
        {
            return string.Join("\n", Doctors.Select(d => d.FullName));
        }
    }
}
