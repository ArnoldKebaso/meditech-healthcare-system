// File: Views/NurseView.xaml.cs

using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace MediTechDesktopApp.Views
{
    /// <summary>
    /// Interaction logic for NurseView.xaml
    /// </summary>
    public partial class NurseView : UserControl
    {
        private readonly NurseService _service;
        private List<Nurse> _nurses;
        private Nurse _currentNurse = null;

        public NurseView()
        {
            InitializeComponent();
            _service = new NurseService();
        }

        /// <summary>
        /// On load: populate department ComboBox and load all nurses into the DataGrid.
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1) Populate Departments lookup (ComboBox)
                DataTable dtDeps = _service.GetAllDepartments();
                cbDepartment.ItemsSource = dtDeps.DefaultView;
                cbDepartment.DisplayMemberPath = "name";
                cbDepartment.SelectedValuePath = "department_id";
                cbDepartment.SelectedIndex = 0;

                // 2) Load all nurses
                RefreshNursesGrid();

                // 3) Make form read-only by default
                SetFormReadOnly(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Nurse form: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Reloads the DataGrid from database and clears the form.
        /// </summary>
        private void RefreshNursesGrid()
        {
            _nurses = _service.GetAllNurses();
            dgNurses.ItemsSource = _nurses;
            ClearFormFields();

            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        /// <summary>
        /// Clears all input fields and resets _currentNurse.
        /// </summary>
        private void ClearFormFields()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            cbDepartment.SelectedIndex = 0;
            txtPhone.Text = "";
            txtEmail.Text = "";
            _currentNurse = null;
        }

        /// <summary>
        /// Enables or disables all input fields (plus Save/New/Refresh).
        /// </summary>
        private void SetFormReadOnly(bool isReadOnly)
        {
            txtFirstName.IsEnabled = !isReadOnly;
            txtLastName.IsEnabled = !isReadOnly;
            cbDepartment.IsEnabled = !isReadOnly;
            txtPhone.IsEnabled = !isReadOnly;
            txtEmail.IsEnabled = !isReadOnly;

            btnSave.IsEnabled = !isReadOnly;
            btnNew.IsEnabled = isReadOnly;
            btnRefresh.IsEnabled = isReadOnly;
        }

        /// <summary>
        /// Fired when the user chooses a row in the DataGrid.
        /// Populates the form fields and enables Edit/Delete.
        /// </summary>
        private void dgNurses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgNurses.SelectedItem is Nurse selected)
            {
                _currentNurse = selected;

                txtFirstName.Text = selected.FirstName;
                txtLastName.Text = selected.LastName;
                cbDepartment.SelectedValue = selected.DepartmentId;
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
        /// “New” button: clear form & allow entry of a brand-new nurse.
        /// </summary>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearFormFields();
            SetFormReadOnly(false);
            _currentNurse = null;
        }

        /// <summary>
        /// “Save” button: if _currentNurse==null → INSERT; otherwise → UPDATE.
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Collect fields
                string first = txtFirstName.Text.Trim();
                string last = txtLastName.Text.Trim();
                if (cbDepartment.SelectedValue == null)
                {
                    MessageBox.Show("Please select a department.", "Validation Error",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                int deptId = (int)cbDepartment.SelectedValue;
                string phone = txtPhone.Text.Trim();
                string email = txtEmail.Text.Trim();

                // Validate required
                if (string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(last))
                {
                    MessageBox.Show("First Name and Last Name are required.", "Validation Error",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_currentNurse == null)
                {
                    // INSERT new Nurse
                    var n = new Nurse
                    {
                        FirstName = first,
                        LastName = last,
                        DepartmentId = deptId,
                        ContactPhone = phone,
                        ContactEmail = email
                    };
                    _service.AddNurse(n);
                    MessageBox.Show("New nurse added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // UPDATE existing Nurse
                    _currentNurse.DepartmentId = deptId;
                    _currentNurse.ContactPhone = phone;
                    _currentNurse.ContactEmail = email;
                    _service.UpdateNurse(_currentNurse);
                    MessageBox.Show("Nurse updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                RefreshNursesGrid();
                SetFormReadOnly(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving nurse: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// “Edit” button: unlock the inputs to modify the selected nurse.
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_currentNurse != null)
                SetFormReadOnly(false);
        }

        /// <summary>
        /// “Delete” button: confirm then delete the selected nurse.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentNurse == null)
                return;

            var result = MessageBox.Show("Are you sure you want to delete this nurse?",
                                         "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _service.DeleteNurse(_currentNurse.NurseId);
                    MessageBox.Show("Nurse deleted.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshNursesGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting nurse: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// “Refresh” button: reload grid & clear form.
        /// </summary>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshNursesGrid();
        }
    }
}
