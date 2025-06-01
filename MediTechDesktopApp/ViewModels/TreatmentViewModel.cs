// File: ViewModels\TreatmentViewModel.cs
using System.Collections.ObjectModel;
using System.Linq;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// ViewModel for Treatments: exposes an ObservableCollection<Treatment> for binding,
    /// and commands to Add/Update/Delete treatments.
    /// </summary>
    public class TreatmentViewModel
    {
        private readonly TreatmentService _service;

        public ObservableCollection<Treatment> Treatments { get; private set; }
            = new ObservableCollection<Treatment>();

        public Treatment SelectedTreatment { get; set; } = new Treatment();

        public TreatmentViewModel()
        {
            _service = new TreatmentService();
            LoadTreatments();
        }

        public void LoadTreatments()
        {
            Treatments.Clear();
            var all = _service.GetAllTreatments();
            foreach (var t in all)
            {
                Treatments.Add(t);
            }
        }

        public void AddTreatment(Treatment newTreatment)
        {
            if (newTreatment == null) return;
            _service.AddTreatment(newTreatment);
            LoadTreatments();
        }

        public void UpdateTreatment(Treatment existingTreatment)
        {
            if (existingTreatment == null || existingTreatment.TreatmentId == 0) return;
            _service.UpdateTreatment(existingTreatment);
            LoadTreatments();
        }

        public void DeleteTreatment(int treatmentId)
        {
            if (treatmentId == 0) return;
            _service.DeleteTreatment(treatmentId);
            LoadTreatments();
        }

        public string GetTreatmentList()
        {
            return string.Join("\n", Treatments.Select(t => $"{t.TreatmentId}: {t.Name}"));
        }
    }
}
