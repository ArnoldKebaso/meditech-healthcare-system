// File: Views/DoctorView.xaml.cs

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
    /// Interaction logic for DoctorView.xaml
    /// </summary>
    public partial class DoctorView : UserControl
    {
        private readonly DoctorService _service;
        private List<Doctor> _doctors;
        private Doctor _currentDoctor = null;

        public DoctorView()
        {
            InitializeComponent();
            _service = new DoctorService();
        }

        /// <summary>
        /// On load: populate specialization ComboBox and load all doctors into the DataGrid.
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1) Populate Specialization lookup (ComboBox)
                DataTable dtSpecs = _service.GetAllSpecializations();
                cbSpecialization.ItemsSource = dtSpecs.DefaultView;
                cbSpecialization.DisplayMemberPath = "name";
                cbSpecialization.SelectedValuePath = "specialization_id";
                cbSpecialization.SelectedIndex = 0;

                // 2) Load all doctors into the grid
                RefreshDoctorsGrid();

                // 3) Make form read-only by default
                SetFormReadOnly(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Doctor form: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Fetches all doctors (JOIN with specialization) and binds to DataGrid.
        /// Also clears the form.
        /// </summary>
        private void RefreshDoctorsGrid()
        {
            _doctors = _service.GetAllDoctors();
            dgDoctors.ItemsSource = _doctors;
            ClearFormFields();

            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        /// <summary>
        /// Clears all input fields and resets _currentDoctor to null.
        /// </summary>
        private void ClearFormFields()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            cbSpecialization.SelectedIndex = 0;
            txtPhone.Text = "";
            txtEmail.Text = "";
            _currentDoctor = null;
        }

        /// <summary>
        /// Enables or disables all input fields (plus Save/New/Refresh buttons).
        /// </summary>
        private void SetFormReadOnly(bool isReadOnly)
        {
            txtFirstName.IsEnabled = !isReadOnly;
            txtLastName.IsEnabled = !isReadOnly;
            cbSpecialization.IsEnabled = !isReadOnly;
            txtPhone.IsEnabled = !isReadOnly;
            txtEmail.IsEnabled = !isReadOnly;

            btnSave.IsEnabled = !isReadOnly;
            btnNew.IsEnabled = isReadOnly;
            btnRefresh.IsEnabled = isReadOnly;
        }

        /// <summary>
        /// Fired when a user clicks on a row in the DataGrid.
        /// Populates the form with that doctor’s data and enables Edit/Delete.
        /// </summary>
        private void dgDoctors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDoctors.SelectedItem is Doctor selected)
            {
                _currentDoctor = selected;

                // Populate form fields
                txtFirstName.Text = selected.FirstName;
                txtLastName.Text = selected.LastName;
                cbSpecialization.SelectedValue = selected.SpecializationId;
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
        /// “New” button: clear form & allow entry of a brand-new doctor.
        /// </summary>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearFormFields();
            SetFormReadOnly(false);
            _currentDoctor = null;
        }

        /// <summary>
        /// “Save” button: if _currentDoctor==null → INSERT via AddDoctor; otherwise → UPDATE via UpdateDoctor.
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Collect data
                string first = txtFirstName.Text.Trim();
                string last = txtLastName.Text.Trim();
                if (cbSpecialization.SelectedValue == null)
                {
                    MessageBox.Show("Please select a specialization.", "Validation Error",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                int specId = (int)cbSpecialization.SelectedValue;
                string phone = txtPhone.Text.Trim();
                string email = txtEmail.Text.Trim();

                // Validate required fields
                if (string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(last))
                {
                    MessageBox.Show("First Name and Last Name are required.", "Validation Error",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_currentDoctor == null)
                {
                    // INSERT new doctor
                    var d = new Doctor
                    {
                        FirstName = first,
                        LastName = last,
                        SpecializationId = specId,
                        ContactPhone = phone,
                        ContactEmail = email
                    };
                    _service.AddDoctor(d);
                    MessageBox.Show("New doctor added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // UPDATE existing doctor
                    _currentDoctor.SpecializationId = specId;
                    _currentDoctor.ContactPhone = phone;
                    _currentDoctor.ContactEmail = email;
                    _service.UpdateDoctor(_currentDoctor);
                    MessageBox.Show("Doctor updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                RefreshDoctorsGrid();
                SetFormReadOnly(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving doctor: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// “Edit” button: allows modifying the selected doctor’s fields.
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_currentDoctor != null)
                SetFormReadOnly(false);
        }

        /// <summary>
        /// “Delete” button: confirm then delete the selected doctor.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentDoctor == null)
                return;

            var result = MessageBox.Show("Are you sure you want to delete this doctor?",
                                         "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _service.DeleteDoctor(_currentDoctor.DoctorId);
                    MessageBox.Show("Doctor deleted.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshDoctorsGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting doctor: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// “Refresh” button: reload all data & clear form.
        /// </summary>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshDoctorsGrid();
        }
    }
}
