using System;
using System.Collections.ObjectModel;
using System.Linq;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// ViewModel for NurseView.xaml.
    /// Exposes an ObservableCollection of Nurse models for data binding in the UI.
    /// Handles Add, Update, Delete, and Refresh logic.
    /// </summary>
    public class NurseViewModel
    {
        private readonly NurseService _service;

        /// <summary>
        /// Collection of all nurses loaded from the database.
        /// The UI binds its DataGrid’s ItemsSource to this property.
        /// </summary>
        public ObservableCollection<Nurse> Nurses { get; private set; }
            = new ObservableCollection<Nurse>();

        /// <summary>
        /// Holds the currently selected nurse (if the UI binds a SelectedItem).
        /// </summary>
        public Nurse SelectedNurse { get; set; } = new Nurse();

        public NurseViewModel()
        {
            _service = new NurseService();
            LoadNurses();
        }

        /// <summary>
        /// Loads all nurses from the service and refreshes the ObservableCollection.
        /// </summary>
        public void LoadNurses()
        {
            Nurses.Clear();
            var allNurses = _service.GetAllNurses();
            foreach (var nurse in allNurses)
            {
                Nurses.Add(nurse);
            }
        }

        /// <summary>
        /// Adds a new nurse via the service and refreshes the list.
        /// </summary>
        public void AddNurse(Nurse newNurse)
        {
            if (newNurse == null)
                return;

            _service.AddNurse(newNurse);
            LoadNurses();
        }

        /// <summary>
        /// Updates an existing nurse via the service and refreshes the list.
        /// </summary>
        public void UpdateNurse(Nurse nurse)
        {
            if (nurse == null || nurse.NurseId == 0)
                return;

            _service.UpdateNurse(nurse);
            LoadNurses();
        }

        /// <summary>
        /// Deletes the selected nurse via the service and refreshes the list.
        /// </summary>
        public void DeleteNurse(Nurse nurse)
        {
            if (nurse == null || nurse.NurseId == 0)
                return;

            _service.DeleteNurse(nurse.NurseId);
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
