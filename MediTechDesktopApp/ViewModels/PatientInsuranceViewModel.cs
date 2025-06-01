// File: ViewModels\PatientInsuranceViewModel.cs
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;
using System;
using System.Collections.ObjectModel;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// VM to handle PatientInsurance list and Add/Update/Delete.
    /// </summary>
    public class PatientInsuranceViewModel
    {
        private readonly PatientInsuranceService _service;

        public ObservableCollection<PatientInsurance> Insurances { get; private set; }
            = new ObservableCollection<PatientInsurance>();

        public PatientInsurance SelectedInsurance { get; set; } = new PatientInsurance();

        public PatientInsuranceViewModel()
        {
            _service = new PatientInsuranceService();
            LoadInsurances();
        }

        public void LoadInsurances()
        {
            Insurances.Clear();
            var all = _service.GetAllPatientInsurance();
            foreach (var ins in all)
                Insurances.Add(ins);
        }

        public void AddInsurance(PatientInsurance ins)
        {
            if (ins == null) return;
            _service.AddPatientInsurance(ins);
            LoadInsurances();
        }

        public void UpdateInsurance()
        {
            if (SelectedInsurance == null) return;
            _service.UpdatePatientInsurance(SelectedInsurance);
            LoadInsurances();
        }

        public void DeleteInsurance()
        {
            if (SelectedInsurance == null) return;
            _service.DeletePatientInsurance(SelectedInsurance);
            LoadInsurances();
        }
    }
}
