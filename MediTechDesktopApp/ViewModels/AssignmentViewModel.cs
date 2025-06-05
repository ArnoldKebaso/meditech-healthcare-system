// File: ViewModels/AssignmentViewModel.cs
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.ViewModels
{
    public class AssignmentViewModel : INotifyPropertyChanged
    {
        // ──── Services ──────────────────────────────────────────────────────────
        private readonly PatientService _patientService;
        private readonly TreatmentService _treatmentService;
        private readonly DoctorService _doctorService;
        private readonly NurseService _nurseService;
        private readonly TreatmentAssignmentService _assignService;

        // ──── Constructor ────────────────────────────────────────────────────────
        public AssignmentViewModel()
        {
            // Designer‐time guard
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;

            // Instantiate commands early
            NewCommand = new RelayCommand(_ => BeginNew(), _ => !IsEditing);
            SaveCommand = new RelayCommand(_ => Save(), _ => CanSave);
            EditCommand = new RelayCommand(_ => BeginEdit(), _ => CanEditOrDelete);
            DeleteCommand = new RelayCommand(_ => Delete(), _ => CanEditOrDelete);
            RefreshCommand = new RelayCommand(_ => RefreshAllAssignments(), _ => true);

            // Wire up services and load data
            _patientService = new PatientService();
            _treatmentService = new TreatmentService();
            _doctorService = new DoctorService();
            _nurseService = new NurseService();
            _assignService = new TreatmentAssignmentService();

            try
            {
                RefreshAllAssignments();
            }
            catch (Exception)
            {
                // swallow any SP/schema mismatch
            }
        }

        // ──── INotifyPropertyChanged ─────────────────────────────────────────────
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        // ──── Public Properties ─────────────────────────────────────────────────

        private ObservableCollection<TreatmentAssignment> _assignments;
        public ObservableCollection<TreatmentAssignment> Assignments
        {
            get => _assignments;
            set { _assignments = value; OnPropertyChanged(nameof(Assignments)); }
        }

        private ObservableCollection<Patient> _patients;
        public ObservableCollection<Patient> Patients
        {
            get => _patients;
            set { _patients = value; OnPropertyChanged(nameof(Patients)); }
        }

        private ObservableCollection<Treatment> _treatments;
        public ObservableCollection<Treatment> Treatments
        {
            get => _treatments;
            set { _treatments = value; OnPropertyChanged(nameof(Treatments)); }
        }

        private ObservableCollection<Doctor> _doctors;
        public ObservableCollection<Doctor> Doctors
        {
            get => _doctors;
            set { _doctors = value; OnPropertyChanged(nameof(Doctors)); }
        }

        private ObservableCollection<Nurse> _nurses;
        public ObservableCollection<Nurse> Nurses
        {
            get => _nurses;
            set { _nurses = value; OnPropertyChanged(nameof(Nurses)); }
        }

        private TreatmentAssignment _currentAssignment;
        public TreatmentAssignment CurrentAssignment
        {
            get => _currentAssignment;
            set
            {
                _currentAssignment = value;
                OnPropertyChanged(nameof(CurrentAssignment));
                OnPropertyChanged(nameof(CanEditOrDelete));
                OnPropertyChanged(nameof(CanSave));
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _isEditing = false;
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
                OnPropertyChanged(nameof(CanEditOrDelete));
                OnPropertyChanged(nameof(CanSave));

                NewCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public bool CanEditOrDelete => CurrentAssignment != null && !IsEditing;

        public bool CanSave
        {
            get
            {
                if (!IsEditing || CurrentAssignment == null)
                    return false;

                return
                    CurrentAssignment.PatientId > 0 &&
                    CurrentAssignment.TreatmentId > 0 &&
                    CurrentAssignment.AssignedDoctorId > 0 &&
                    CurrentAssignment.AssignedNurseId > 0 &&
                    CurrentAssignment.AssignmentDate != default(DateTime);
            }
        }

        // ──── Commands ─────────────────────────────────────────────────────────────

        public RelayCommand NewCommand { get; }
        public RelayCommand SaveCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand RefreshCommand { get; }

        // ──── Command Handlers ────────────────────────────────────────────────────

        private void BeginNew()
        {
            CurrentAssignment = new TreatmentAssignment
            {
                AssignmentDate = DateTime.Now
            };
            IsEditing = true;
        }

        private void BeginEdit()
        {
            if (CurrentAssignment != null)
                IsEditing = true;
        }

        private void Save()
        {
            if (CurrentAssignment == null) return;

            try
            {
                if (CurrentAssignment.PatientId == 0)
                    _assignService.AddAssignment(CurrentAssignment);
                else
                    _assignService.UpdateAssignment(CurrentAssignment);

                RefreshAllAssignments();
                IsEditing = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving assignment: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete()
        {
            if (CurrentAssignment == null) return;

            var res = MessageBox.Show(
                "Delete this assignment?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    _assignService.DeleteAssignment(CurrentAssignment);
                    RefreshAllAssignments();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting assignment: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RefreshAllAssignments()
        {
            // Pull in all lookup data (Patients, Treatments, Doctors, Nurses):
            Patients = new ObservableCollection<Patient>(_patientService.GetAllPatients());
            Treatments = new ObservableCollection<Treatment>(_treatmentService.GetAllTreatments());
            Doctors = new ObservableCollection<Doctor>(_doctorService.GetAllDoctors());
            Nurses = new ObservableCollection<Nurse>(_nurseService.GetAllNurses());

            // Now get the existing assignments:
            var list = _assignService.GetAllAssignments();
            Assignments = new ObservableCollection<TreatmentAssignment>(list);

            CurrentAssignment = null;
            IsEditing = false;
        }
    }
}
