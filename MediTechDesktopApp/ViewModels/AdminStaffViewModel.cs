// File: ViewModels\AdminStaffViewModel.cs
using System.Collections.ObjectModel;
using System.Linq;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// ViewModel for AdminStaff: exposes an ObservableCollection<AdminStaff> for binding,
    /// and commands to Add/Update/Delete records.
    /// </summary>
    public class AdminStaffViewModel
    {
        private readonly AdminStaffService _service;

        public ObservableCollection<AdminStaff> Admins { get; private set; }
            = new ObservableCollection<AdminStaff>();

        public AdminStaff SelectedAdmin { get; set; } = new AdminStaff();

        public AdminStaffViewModel()
        {
            _service = new AdminStaffService();
            LoadAdmins();
        }

        /// <summary>
        /// Clears the current collection and reloads from the database.
        /// </summary>
        public void LoadAdmins()
        {
            Admins.Clear();
            var all = _service.GetAllAdmins();
            foreach (var a in all)
            {
                Admins.Add(a);
            }
        }

        /// <summary>
        /// Inserts a new admin record and refreshes.
        /// </summary>
        public void AddAdmin(AdminStaff newAdmin)
        {
            if (newAdmin == null) return;
            _service.AddAdmin(newAdmin);
            LoadAdmins();
        }

        /// <summary>
        /// Updates an existing admin record and refreshes.
        /// </summary>
        public void UpdateAdmin(AdminStaff existingAdmin)
        {
            if (existingAdmin == null || existingAdmin.StaffId == 0) return;
            _service.UpdateAdmin(existingAdmin);
            LoadAdmins();
        }

        /// <summary>
        /// Deletes the selected admin by ID and refreshes.
        /// </summary>
        public void DeleteAdmin(int staffId)
        {
            if (staffId == 0) return;
            _service.DeleteAdmin(staffId);
            LoadAdmins();
        }

        /// <summary>
        /// Returns a newline‐separated list of all admin full names.
        /// </summary>
        public string GetAdminNames()
        {
            return string.Join("\n", Admins.Select(a => a.FullName));
        }
    }
}
