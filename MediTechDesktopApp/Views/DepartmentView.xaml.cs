using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.Views
{
    /// <summary>
    /// Interaction logic for DepartmentView.xaml
    /// </summary>
    public partial class DepartmentView : UserControl
    {
        private readonly DepartmentService _service;
        private List<Department> _allDepartments;
        private Department _currentDepartment;

        public DepartmentView()
        {
            InitializeComponent();
            _service = new DepartmentService();
        }

        /// <summary>
        /// Loaded event: populate DataGrid and set form read-only.
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshDepartmentGrid();
                SetFormReadOnly(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Department form: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Reload the grid from the service, clear form.
        /// </summary>
        private void RefreshDepartmentGrid()
        {
            _allDepartments = _service.GetAllDepartments();
            dgDepartments.ItemsSource = _allDepartments;

            ClearFormFields();
            SetFormReadOnly(true);
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        /// <summary>
        /// Clear the single Name field and reset _currentDepartment.
        /// </summary>
        private void ClearFormFields()
        {
            txtName.Text = "";
            _currentDepartment = null;
        }

        /// <summary>
        /// Enable/disable the Name textbox and Save button.
        /// </summary>
        private void SetFormReadOnly(bool isReadOnly)
        {
            txtName.IsEnabled = !isReadOnly;
            btnSave.IsEnabled = !isReadOnly;
            btnNew.IsEnabled = isReadOnly;
            btnRefresh.IsEnabled = isReadOnly;
        }

        /// <summary>
        /// Fired when user selects a row. Populate Name field.
        /// </summary>
        private void dgDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDepartments.SelectedItem is Department selected)
            {
                _currentDepartment = selected;
                txtName.Text = selected.Name;

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
        /// “New” button: clear and allow editing.
        /// </summary>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearFormFields();
            SetFormReadOnly(false);
            _currentDepartment = null;
        }

        /// <summary>
        /// “Save” button: either Add or Update via service.
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtName.Text.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Name is required.",
                                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_currentDepartment == null)
                {
                    // INSERT
                    var d = new Department { Name = name };
                    _service.AddDepartment(d);
                    MessageBox.Show("New department added.",
                                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // UPDATE
                    _currentDepartment.Name = name;
                    _service.UpdateDepartment(_currentDepartment);
                    MessageBox.Show("Department updated.",
                                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                RefreshDepartmentGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving department: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// “Edit” button: enable editing of the single Name textbox.
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_currentDepartment != null)
                SetFormReadOnly(false);
        }

        /// <summary>
        /// “Delete” button: confirm then delete.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentDepartment == null) return;

            var result = MessageBox.Show(
                "Are you sure you want to delete this department?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _service.DeleteDepartment(_currentDepartment.DepartmentId);
                    MessageBox.Show("Department deleted.",
                                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshDepartmentGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting department: {ex.Message}",
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// “Refresh” button: simply reload grid & clear form.
        /// </summary>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshDepartmentGrid();
        }
    }
}
