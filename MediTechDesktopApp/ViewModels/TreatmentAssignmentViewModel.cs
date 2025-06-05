// File: ViewModels\TreatmentAssignmentViewModel.cs
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;
using System;
using System.Collections.ObjectModel;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// VM to expose an ObservableCollection of TreatmentAssignment and handle Add + Reload.
    /// </summary>
    public class TreatmentAssignmentViewModel
    {
        private readonly TreatmentAssignmentService _service;

        public ObservableCollection<TreatmentAssignment> Assignments { get; private set; }
            = new ObservableCollection<TreatmentAssignment>();

        public TreatmentAssignment SelectedAssignment { get; set; } = new TreatmentAssignment();

        public TreatmentAssignmentViewModel()
        {
            _service = new TreatmentAssignmentService();
            LoadAssignments();
        }

        public void LoadAssignments()
        {
            Assignments.Clear();
            var all = _service.GetAllAssignments();
            foreach (var item in all)
                Assignments.Add(item);
        }

        public void AddAssignment(TreatmentAssignment newA)
        {
            if (newA == null) return;

            _service.AddAssignment(newA);
            LoadAssignments();
        }
    }
}
