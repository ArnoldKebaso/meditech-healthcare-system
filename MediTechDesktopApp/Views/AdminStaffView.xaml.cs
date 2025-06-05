using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.Views
{
    /// <summary>
    /// Interaction logic for AdminStaffView.xaml
    /// </summary>
    public partial class AdminStaffView : UserControl
    {
        private readonly AdminStaffService _service;
        private readonly StaffRoleService _roleService;

        private List<AdminStaff> _allAdmins;   // local cache of AdminStaff rows
        private AdminStaff _currentAdmin;      // currently selected or being edited

        public AdminStaffView()
        {
            InitializeComponent();
            _service = new AdminStaffService();
            _roleService = new StaffRoleService();
        }

        /// <summary>
        /// Loaded event: populate roles drop‐down and grid.
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadRoles();          // populate the cbRole ComboBox
                RefreshAdminGrid();   // populate the DataGrid
                SetFormReadOnly(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing AdminStaff form: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Loads all roles into cbRole.
        /// </summary>
        private void LoadRoles()
        {
            var roles = _roleService.GetAllRoles();
            cbRole.ItemsSource = roles;
            cbRole.SelectedIndex = -1;
        }

        /// <summary>
        /// Fetches all AdminStaff rows from DB and binds to DataGrid.
        /// Also resets form to blank.
        /// </summary>
        private void RefreshAdminGrid()
        {
            _allAdmins = _service.GetAllAdmins();
            dgAdminStaff.ItemsSource = _allAdmins;

            ClearForm();
            SetFormReadOnly(true);
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        /// <summary>
        /// Clears form fields and resets the current object.
        /// </summary>
        private void ClearForm()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            cbRole.SelectedIndex = -1;
            txtPhone.Text = "";
            txtEmail.Text = "";
            _currentAdmin = null;
        }

        /// <summary>
        /// Enables or disables all input fields/buttons.
        /// </summary>
        private void SetFormReadOnly(bool isReadOnly)
        {
            txtFirstName.IsEnabled = !isReadOnly;
            txtLastName.IsEnabled = !isReadOnly;
            cbRole.IsEnabled = !isReadOnly;
            txtPhone.IsEnabled = !isReadOnly;
            txtEmail.IsEnabled = !isReadOnly;

            btnSave.IsEnabled = !isReadOnly;
            btnNew.IsEnabled = isReadOnly;
            btnRefresh.IsEnabled = isReadOnly;
        }

        /// <summary>
        /// Fired when the user selects a row in the DataGrid.
        /// Populates the form fields with that AdminStaff.
        /// </summary>
        private void dgAdminStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAdminStaff.SelectedItem is AdminStaff selected)
            {
                _currentAdmin = selected;

                txtFirstName.Text = selected.FirstName;
                txtLastName.Text = selected.LastName;

                // Select the correct item in cbRole by RoleId
                for (int i = 0; i < cbRole.Items.Count; i++)
                {
                    if (((StaffRole)cbRole.Items[i]).RoleId == selected.RoleId)
                    {
                        cbRole.SelectedIndex = i;
                        break;
                    }
                }

                txtPhone.Text = selected.ContactPhone;
                txtEmail.Text = selected.ContactEmail;

                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
            else
            {
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
        }

        /// <summary>
        /// “New” button: clears form and allows entry of a brand-new AdminStaff.
        /// </summary>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            SetFormReadOnly(false);
            _currentAdmin = null;
        }

        /// <summary>
        /// “Save” button: validates required fields, then either inserts or updates.
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1) Gather + validate
                string first = txtFirstName.Text.Trim();
                string last = txtLastName.Text.Trim();

                if (string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(last))
                {
                    MessageBox.Show("First Name and Last Name are required.",
                                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (cbRole.SelectedItem == null)
                {
                    MessageBox.Show("Please select a Role.",
                                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int roleId = ((StaffRole)cbRole.SelectedItem).RoleId;
                string phone = txtPhone.Text.Trim();
                string email = txtEmail.Text.Trim();

                // 2) If currentAdmin is null → INSERT
                if (_currentAdmin == null)
                {
                    var newAdmin = new AdminStaff
                    {
                        FirstName = first,
                        LastName = last,
                        RoleId = roleId,
                        ContactPhone = phone,
                        ContactEmail = email
                    };

                    _service.AddAdmin(newAdmin);
                    MessageBox.Show("New admin staff added successfully.",
                                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // 3) Otherwise → UPDATE
                    _currentAdmin.FirstName = first;
                    _currentAdmin.LastName = last;
                    _currentAdmin.RoleId = roleId;
                    _currentAdmin.ContactPhone = phone;
                    _currentAdmin.ContactEmail = email;

                    _service.UpdateAdmin(_currentAdmin);
                    MessageBox.Show("Admin staff updated successfully.",
                                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                RefreshAdminGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving AdminStaff: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// “Edit” button: simply re-enables input fields so the user can modify.
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_currentAdmin != null)
                SetFormReadOnly(false);
        }

        /// <summary>
        /// “Delete” button: confirm and delete the selected record.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentAdmin == null) return;

            var result = MessageBox.Show(
                "Are you sure you want to delete this admin staff record?",
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _service.DeleteAdmin(_currentAdmin.StaffId);
                    MessageBox.Show("Admin staff deleted.",
                                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshAdminGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting AdminStaff: {ex.Message}",
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// “Refresh” button: simply reload grid and clear form.
        /// </summary>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshAdminGrid();
        }
    }
}
