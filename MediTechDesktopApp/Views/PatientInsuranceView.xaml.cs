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
    /// Interaction logic for PatientInsuranceView.xaml
    /// </summary>
    public partial class PatientInsuranceView : UserControl
    {
        private readonly PatientInsuranceService _service;
        private List<PatientInsurance> _allItems;
        private PatientInsurance _currentItem = null;

        public PatientInsuranceView()
        {
            InitializeComponent();
            _service = new PatientInsuranceService();
        }

        /// <summary>
        /// On load, fill DataGrid + ComboBoxes.
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadComboBoxes();
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Patient Insurance form: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Loads patients and policies lists into cbPatient and cbPolicy.
        /// </summary>
        private void LoadComboBoxes()
        {
            // 1) Patients
            var patients = _service.GetAllPatientsForCombo();
            cbPatient.ItemsSource = patients;
            cbPatient.DisplayMemberPath = "Name";    // Now references ComboItem.Name
            cbPatient.SelectedValuePath = "Id";      // Now references ComboItem.Id

            // 2) Policies
            var policies = _service.GetAllPoliciesForCombo();
            cbPolicy.ItemsSource = policies;
            cbPolicy.DisplayMemberPath = "Name";     // ComboItem.Name holds "policy_number"
            cbPolicy.SelectedValuePath = "Id";       // ComboItem.Id holds "policy_id"
        }

        /// <summary>
        /// Fetch all rows, bind to grid, clear form.
        /// </summary>
        private void RefreshGrid()
        {
            _allItems = _service.GetAllPatientInsurance();
            dgPatientInsurance.ItemsSource = _allItems;

            ClearForm();
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = false;
        }

        /// <summary>
        /// Clears the ComboBoxes and resets current.
        /// </summary>
        private void ClearForm()
        {
            cbPatient.SelectedIndex = -1;
            cbPolicy.SelectedIndex = -1;
            _currentItem = null;
            btnNew.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        /// <summary>
        /// Called when a row is selected in the DataGrid.
        /// Populates ComboBoxes.
        /// </summary>
        private void dgPatientInsurance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPatientInsurance.SelectedItem is PatientInsurance selected)
            {
                _currentItem = selected;
                // Set selected values on the ComboBoxes
                cbPatient.SelectedValue = selected.PatientId;  // matches Id
                cbPolicy.SelectedValue = selected.PolicyId;    // matches Id
                btnDelete.IsEnabled = true;
                btnSave.IsEnabled = false; // save only on “New”
            }
            else
            {
                btnDelete.IsEnabled = false;
                btnSave.IsEnabled = false;
            }
        }

        /// <summary>
        /// “New” – clears form so user can pick new patient & policy.
        /// </summary>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            btnSave.IsEnabled = true; // allow Save now
            btnDelete.IsEnabled = false;
        }

        /// <summary>
        /// “Save” – inserts a new row if no current, otherwise do nothing.
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validation: both must be selected
                if (cbPatient.SelectedIndex < 0 || cbPolicy.SelectedIndex < 0)
                {
                    MessageBox.Show("Please choose a Patient and a Policy before saving.",
                                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int patientId = (int)cbPatient.SelectedValue;
                int policyId = (int)cbPolicy.SelectedValue;

                // Prevent duplicate
                if (_allItems.Any(pi => pi.PatientId == patientId && pi.PolicyId == policyId))
                {
                    MessageBox.Show("That Patient + Policy pair already exists.",
                                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newItem = new PatientInsurance
                {
                    PatientId = patientId,
                    PolicyId = policyId
                };
                _service.AddPatientInsurance(newItem);
                MessageBox.Show("New Patient Insurance record added.",
                                "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving Patient Insurance: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// “Delete” – deletes selected composite‐key row.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentItem == null) return;

            var result = MessageBox.Show("Are you sure you want to delete this Patient Insurance record?",
                                         "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _service.DeletePatientInsurance(_currentItem.PatientId, _currentItem.PolicyId);
                    MessageBox.Show("Patient Insurance deleted.",
                                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting Patient Insurance: {ex.Message}",
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// “Refresh” – reloads everything.
        /// </summary>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadComboBoxes();
            RefreshGrid();
        }
    }
}
