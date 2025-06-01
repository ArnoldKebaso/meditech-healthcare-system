// File: ViewModels\NurseViewModel.cs
using System.Collections.ObjectModel;
using System.Linq;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// ViewModel to handle nurse CRUD operations and expose an observable collection
    /// of Nurse models for data binding in the UI.
    /// </summary>
    public class NurseViewModel
    {
        private readonly NurseService _service;

        /// <summary>
        /// Collection of all nurses retrieved from the database.
        /// The UI binds its DataGrid’s ItemsSource to this property.
        /// </summary>
        public ObservableCollection<Nurse> Nurses { get; private set; }
            = new ObservableCollection<Nurse>();

        /// <summary>
        /// Holds the currently selected Nurse (if the UI binds a SelectedItem).
        /// </summary>
        public Nurse SelectedNurse { get; set; } = new Nurse();

        public NurseViewModel()
        {
            _service = new NurseService();
            LoadNurses();
        }

        /// <summary>
        /// Clears the existing collection and repopulates it.
        /// </summary>
        public void LoadNurses()
        {
            Nurses.Clear();
            var allNurses = _service.GetAllNurses();
            foreach (var n in allNurses)
            {
                Nurses.Add(n);
            }
        }

        /// <summary>
        /// Inserts a new nurse into the database and reloads the collection.
        /// </summary>
        public void AddNurse(Nurse newNurse)
        {
            if (newNurse == null)
                return;

            _service.AddNurse(newNurse);
            LoadNurses();
        }

        /// <summary>
        /// Updates an existing nurse and reloads.
        /// </summary>
        public void UpdateNurse(Nurse nurseToUpdate)
        {
            if (nurseToUpdate == null || nurseToUpdate.NurseId == 0)
                return;

            _service.UpdateNurse(nurseToUpdate);
            LoadNurses();
        }

        /// <summary>
        /// Deletes the selected nurse from the database and reloads.
        /// </summary>
        public void DeleteNurse(int nurseId)
        {
            if (nurseId == 0)
                return;

            _service.DeleteNurse(nurseId);
            LoadNurses();
        }

        /// <summary>
        /// Returns a newline‐separated list of all nurse full names.
        /// </summary>
        public string GetNurseNames()
        {
            return string.Join("\n", Nurses.Select(n => n.FullName));
        }
    }
}
