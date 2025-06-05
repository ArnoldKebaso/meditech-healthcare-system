// File: Views/PatientView.xaml.cs

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
    /// Interaction logic for PatientView.xaml
    /// </summary>
    public partial class PatientView : UserControl
    {
        private readonly PatientService _service;
        private List<Patient> _patients;      // local cache of all Patient rows
        private Patient _currentPatient = null;

        public PatientView()
        {
            InitializeComponent();
            _service = new PatientService();
        }

        /// <summary>
        /// Loaded event: populates the DataGrid with all patients.
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshPatientsGrid();
                SetFormReadOnly(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Patient form: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Loads all patients from the service and binds the DataGrid.
        /// Also clears the form fields.
        /// </summary>
        private void RefreshPatientsGrid()
        {
            // 1) Fetch all patients into a List<Patient>
            _patients = _service.GetAllPatients();

            // 2) Bind to DataGrid
            dgPatients.ItemsSource = _patients;

            // 3) Clear form
            ClearFormFields();
            SetFormReadOnly(true);
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        /// <summary>
        /// Clears all input textboxes/comboBoxes
        /// and resets currentPatient to null.
        /// </summary>
        private void ClearFormFields()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            dpDateOfBirth.SelectedDate = null;
            cbGender.SelectedIndex = -1;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            _currentPatient = null;
        }

        /// <summary>
        /// Enables or disables all input fields.
        /// </summary>
        private void SetFormReadOnly(bool isReadOnly)
        {
            txtFirstName.IsEnabled = !isReadOnly;
            txtLastName.IsEnabled = !isReadOnly;
            dpDateOfBirth.IsEnabled = !isReadOnly;
            cbGender.IsEnabled = !isReadOnly;
            txtPhone.IsEnabled = !isReadOnly;
            txtEmail.IsEnabled = !isReadOnly;
            txtAddress.IsEnabled = !isReadOnly;

            btnSave.IsEnabled = !isReadOnly;
            btnNew.IsEnabled = isReadOnly;
            btnRefresh.IsEnabled = isReadOnly;
        }

        /// <summary>
        /// Fired when user selects a row in the Patients DataGrid.
        /// Populates the form fields with the selected Patient.
        /// </summary>
        private void dgPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPatients.SelectedItem is Patient selected)
            {
                _currentPatient = selected;

                // Fill form fields
                txtFirstName.Text = selected.FirstName;
                txtLastName.Text = selected.LastName;
                dpDateOfBirth.SelectedDate = selected.DateOfBirth;
                cbGender.Text = selected.Gender;
                txtPhone.Text = selected.ContactPhone;
                txtEmail.Text = selected.ContactEmail;
                txtAddress.Text = selected.Address;

                // Enable Edit/Delete buttons
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
        /// “New” button: clear form and allow entry of a brand-new Patient.
        /// </summary>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearFormFields();
            SetFormReadOnly(false);
            _currentPatient = null; // we are creating a new one
        }

        /// <summary>
        /// “Save” button: either inserts (if _currentPatient==null) or updates existing.
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1) Validate required fields
                string first = txtFirstName.Text.Trim();
                string last = txtLastName.Text.Trim();
                DateTime? dob = dpDateOfBirth.SelectedDate;
                string gender = (cbGender.SelectedItem as ComboBoxItem)?.Content.ToString();
                string phone = txtPhone.Text.Trim();
                string email = txtEmail.Text.Trim();
                string address = txtAddress.Text.Trim();

                if (string.IsNullOrWhiteSpace(first) ||
                    string.IsNullOrWhiteSpace(last) ||
                    dob == null ||
                    string.IsNullOrWhiteSpace(gender))
                {
                    MessageBox.Show("First Name, Last Name, Date of Birth, and Gender are required.",
                                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_currentPatient == null)
                {
                    // INSERT new Patient
                    var p = new Patient
                    {
                        FirstName = first,
                        LastName = last,
                        DateOfBirth = dob.Value,
                        Gender = gender,
                        ContactPhone = phone,
                        ContactEmail = email,
                        Address = address
                    };
                    _service.AddPatient(p);
                    MessageBox.Show("New patient added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // UPDATE existing Patient
                    _currentPatient.ContactPhone = phone;
                    _currentPatient.ContactEmail = email;
                    _currentPatient.Address = address;
                    _service.UpdatePatient(_currentPatient);
                    MessageBox.Show("Patient updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                RefreshPatientsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving patient: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// “Edit” button: unlock the inputs so user can change phone/email/address.
        /// (We do not allow editing first/last/dob/gender after creation.)
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPatient != null)
                SetFormReadOnly(false);
        }

        /// <summary>
        /// “Delete” button: confirm, then delete the selected patient.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPatient == null)
                return;

            var result = MessageBox.Show("Are you sure you want to delete this patient?",
                                         "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _service.DeletePatient(_currentPatient.PatientId);
                    MessageBox.Show("Patient deleted.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshPatientsGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting patient: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// “Refresh” button: simply reload the grid & clear form.
        /// </summary>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshPatientsGrid();
        }
    }
}
