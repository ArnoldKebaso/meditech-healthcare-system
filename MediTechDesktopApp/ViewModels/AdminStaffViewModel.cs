// File: ViewModels/AdminStaffViewModel.cs
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.ViewModels
{
    public class AdminStaffViewModel : INotifyPropertyChanged
    {
        // ──── Services ──────────────────────────────────────────────────────────
        private readonly StaffRoleService _roleService;
        private readonly AdminStaffService _adminService;

        // ──── Constructor ────────────────────────────────────────────────────────
        public AdminStaffViewModel()
        {
            // 1) Designer‐time guard (so Visual Studio’s XAML designer does not hit DB)
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                return;
            }

            // 2) Instantiate commands BEFORE we ever set CurrentAdmin or call RefreshAllAdmins()
            NewCommand = new RelayCommand(_ => BeginNew(), _ => !IsEditing);
            SaveCommand = new RelayCommand(_ => Save(), _ => CanSave);
            EditCommand = new RelayCommand(_ => BeginEdit(), _ => CanEditOrDelete);
            DeleteCommand = new RelayCommand(_ => Delete(), _ => CanEditOrDelete);
            RefreshCommand = new RelayCommand(_ => RefreshAllAdmins(), _ => true);

            // 3) Now that commands exist, wire up services and load data:
            _roleService = new StaffRoleService();
            _adminService = new AdminStaffService();

            try
            {
                LoadRoles();
                RefreshAllAdmins();
            }
            catch (Exception ex)
            {
                // swallow any schema/SP mismatch errors
                // (you’ll see an empty Roles/Admins list instead of a crash)
                // later, once your SP is fixed, remove this catch.
            }
        }


        // ──── INotifyPropertyChanged ─────────────────────────────────────────────
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));


        // ──── Public Properties ───────────────────────────────────────────────────

        private ObservableCollection<StaffRole> _roles;
        public ObservableCollection<StaffRole> Roles
        {
            get => _roles;
            set { _roles = value; OnPropertyChanged(nameof(Roles)); }
        }

        private ObservableCollection<AdminStaff> _admins;
        public ObservableCollection<AdminStaff> Admins
        {
            get => _admins;
            set { _admins = value; OnPropertyChanged(nameof(Admins)); }
        }

        private AdminStaff _currentAdmin;
        public AdminStaff CurrentAdmin
        {
            get => _currentAdmin;
            set
            {
                _currentAdmin = value;
                OnPropertyChanged(nameof(CurrentAdmin));
                OnPropertyChanged(nameof(CanEditOrDelete));
                OnPropertyChanged(nameof(CanSave));
                // at this point, SaveCommand is already non‐null (because we moved instantiation to top),
                // so calling RaiseCanExecuteChanged() is safe.
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

                // update each command’s CanExecute
                NewCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// True if one row is selected AND we are not editing right now → enables Edit/Delete.
        /// </summary>
        public bool CanEditOrDelete => CurrentAdmin != null && !IsEditing;

        /// <summary>
        /// True if we are in editing mode AND required fields are valid → enables Save.
        /// </summary>
        public bool CanSave
        {
            get
            {
                if (!IsEditing || CurrentAdmin == null)
                    return false;

                return
                    !string.IsNullOrWhiteSpace(CurrentAdmin.FirstName) &&
                    !string.IsNullOrWhiteSpace(CurrentAdmin.LastName) &&
                    CurrentAdmin.RoleId > 0;
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
            CurrentAdmin = new AdminStaff();
            IsEditing = true;
        }

        private void BeginEdit()
        {
            if (CurrentAdmin != null)
                IsEditing = true;
        }

        private void Save()
        {
            if (CurrentAdmin == null) return;

            try
            {
                if (CurrentAdmin.StaffId == 0)
                {
                    _adminService.AddAdmin(CurrentAdmin);
                }
                else
                {
                    _adminService.UpdateAdmin(CurrentAdmin);
                }

                RefreshAllAdmins();
                IsEditing = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving AdminStaff: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete()
        {
            if (CurrentAdmin == null) return;

            var res = MessageBox.Show(
                "Delete this admin staff?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    _adminService.DeleteAdmin(CurrentAdmin.StaffId);
                    RefreshAllAdmins();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting AdminStaff: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RefreshAllAdmins()
        {
            // We already instantiated _roleService and _adminService above.
            var rolesList = _roleService.GetAllRoles();
            Roles = new ObservableCollection<StaffRole>(rolesList);

            var adminList = _adminService.GetAllAdmins();
            Admins = new ObservableCollection<AdminStaff>(adminList);

            CurrentAdmin = null;
            IsEditing = false;
        }


        // ──── Private Helpers ─────────────────────────────────────────────────────
        private void LoadRoles()
        {
            var list = _roleService.GetAllRoles();
            Roles = new ObservableCollection<StaffRole>(list);
        }
    }
}
