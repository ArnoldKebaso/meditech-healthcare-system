using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.ViewModels
{
    public class TreatmentViewModel : INotifyPropertyChanged
    {
        private readonly TreatmentService _service;

        public ObservableCollection<Treatment> Treatments { get; private set; }
            = new ObservableCollection<Treatment>();

        private Treatment _currentTreatment;
        public Treatment CurrentTreatment
        {
            get => _currentTreatment;
            set
            {
                _currentTreatment = value;
                OnPropertyChanged(nameof(CurrentTreatment));
                SaveCommand?.RaiseCanExecuteChanged();
                EditCommand?.RaiseCanExecuteChanged();
                DeleteCommand?.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand NewCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand EditCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand RefreshCommand { get; private set; }

        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
                SaveCommand?.RaiseCanExecuteChanged();
                EditCommand?.RaiseCanExecuteChanged();
                DeleteCommand?.RaiseCanExecuteChanged();
                NewCommand?.RaiseCanExecuteChanged();
            }
        }

        public TreatmentViewModel()
        {
            _service = new TreatmentService();

            // Initialize commands (so that they are never null)
            NewCommand = new RelayCommand(_ => BeginNew(), _ => !IsEditing);
            EditCommand = new RelayCommand(_ => BeginEdit(), _ => CurrentTreatment != null && !IsEditing);
            SaveCommand = new RelayCommand(_ => Save(), _ => CanSave());
            DeleteCommand = new RelayCommand(_ => Delete(), _ => CurrentTreatment != null && !IsEditing);
            RefreshCommand = new RelayCommand(_ => RefreshAll(), _ => !IsEditing);

            // Load data into grid
            RefreshAll();
        }

        private void RefreshAll()
        {
            Treatments.Clear();
            foreach (var t in _service.GetAllTreatments())
            {
                Treatments.Add(t);
            }
            IsEditing = false;
            CurrentTreatment = null;
        }

        private void BeginNew()
        {
            CurrentTreatment = new Treatment();
            IsEditing = true;
        }

        private void BeginEdit()
        {
            if (CurrentTreatment != null)
                IsEditing = true;
        }

        private bool CanSave()
        {
            if (!IsEditing) return false;
            if (CurrentTreatment == null) return false;
            // At minimum, “Name” must not be blank
            return !string.IsNullOrWhiteSpace(CurrentTreatment.Name);
        }

        private void Save()
        {
            if (CurrentTreatment == null) return;

            if (CurrentTreatment.TreatmentId == 0)
            {
                _service.AddTreatment(CurrentTreatment);
            }
            else
            {
                _service.UpdateTreatment(CurrentTreatment);
            }
            RefreshAll();
        }

        private void Delete()
        {
            if (CurrentTreatment == null) return;
            _service.DeleteTreatment(CurrentTreatment.TreatmentId);
            RefreshAll();
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
