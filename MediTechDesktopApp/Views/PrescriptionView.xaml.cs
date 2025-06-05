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
    /// Interaction logic for PrescriptionView.xaml
    /// </summary>
    public partial class PrescriptionView : UserControl
    {
        private readonly PrescriptionService _service;
        private List<Prescription> _prescriptions;
        private Prescription _currentPrescription = null;

        public PrescriptionView()
        {
            InitializeComponent();
            _service = new PrescriptionService();
        }

        /// <summary>
        /// Loaded event: populates DataGrid and ComboBoxes.
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshPrescriptionGrid();
                SetFormReadOnly(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Prescription form: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Loads all prescriptions; sets up record and frequency ComboBoxes.
        /// </summary>
        private void RefreshPrescriptionGrid()
        {
            // 1) Fetch prescriptions
            _prescriptions = _service.GetAllPrescriptions();
            dgPrescriptions.ItemsSource = _prescriptions;

            // 2) Populate Records dropdown
            cbRecords.ItemsSource = _service.GetAllMedicalRecordsForCombo()
                                    .Select(x => new ComboBoxItem
                                    {
                                        Content = x.display,
                                        Tag = x.id
                                    }).ToList();

            // 3) Populate Frequency dropdown
            cbFrequency.ItemsSource = _service.GetAllFrequencyTypesForCombo()
                                      .Select(x => new ComboBoxItem
                                      {
                                          Content = x.name,
                                          Tag = x.id
                                      }).ToList();

            // 4) Clear form & disable editing
            ClearFormFields();
            SetFormReadOnly(true);
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        /// <summary>
        /// Clears all form inputs.
        /// </summary>
        private void ClearFormFields()
        {
            cbRecords.SelectedIndex = -1;
            txtMedicationName.Text = "";
            txtDosage.Text = "";
            cbFrequency.SelectedIndex = -1;
            dpStartDate.SelectedDate = null;
            dpEndDate.SelectedDate = null;
            txtInstructions.Text = "";
            _currentPrescription = null;
        }

        /// <summary>
        /// Enable/disable form inputs and Save/New buttons.
        /// </summary>
        private void SetFormReadOnly(bool isReadOnly)
        {
            cbRecords.IsEnabled = !isReadOnly;
            txtMedicationName.IsEnabled = !isReadOnly;
            txtDosage.IsEnabled = !isReadOnly;
            cbFrequency.IsEnabled = !isReadOnly;
            dpStartDate.IsEnabled = !isReadOnly;
            dpEndDate.IsEnabled = !isReadOnly;
            txtInstructions.IsEnabled = !isReadOnly;

            btnSave.IsEnabled = !isReadOnly;
            btnNew.IsEnabled = isReadOnly;
            btnRefresh.IsEnabled = isReadOnly;
        }

        /// <summary>
        /// When user selects a row, populate the form.
        /// </summary>
        private void dgPrescriptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPrescriptions.SelectedItem is Prescription sel)
            {
                _currentPrescription = sel;

                // Select appropriate items in ComboBoxes by Tag
                foreach (ComboBoxItem item in cbRecords.Items)
                {
                    if ((int)item.Tag == sel.RecordId)
                    {
                        cbRecords.SelectedItem = item;
                        break;
                    }
                }
                txtMedicationName.Text = sel.MedicationName;
                txtDosage.Text = sel.Dosage;
                foreach (ComboBoxItem item in cbFrequency.Items)
                {
                    if ((int)item.Tag == sel.FrequencyId)
                    {
                        cbFrequency.SelectedItem = item;
                        break;
                    }
                }
                dpStartDate.SelectedDate = sel.StartDate;
                dpEndDate.SelectedDate = sel.EndDate;
                txtInstructions.Text = sel.Instructions;

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
        /// “New”: clear form & enable entry.
        /// </summary>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearFormFields();
            SetFormReadOnly(false);
            _currentPrescription = null;
        }

        /// <summary>
        /// “Save”: Insert or Update prescription.
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1) Validate required fields
                if (cbRecords.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(txtMedicationName.Text) ||
                    string.IsNullOrWhiteSpace(txtDosage.Text) ||
                    cbFrequency.SelectedItem == null)
                {
                    MessageBox.Show("You must select a Record, enter Medication, Dosage, and pick a Frequency.",
                                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int recordId = (int)((ComboBoxItem)cbRecords.SelectedItem).Tag;
                string medName = txtMedicationName.Text.Trim();
                string dosage = txtDosage.Text.Trim();
                int frequencyId = (int)((ComboBoxItem)cbFrequency.SelectedItem).Tag;
                DateTime? startDate = dpStartDate.SelectedDate;
                DateTime? endDate = dpEndDate.SelectedDate;
                string instructions = txtInstructions.Text.Trim();

                if (_currentPrescription == null)
                {
                    // INSERT
                    var pr = new Prescription
                    {
                        RecordId = recordId,
                        MedicationName = medName,
                        Dosage = dosage,
                        FrequencyId = frequencyId,
                        StartDate = startDate,
                        EndDate = endDate,
                        Instructions = instructions
                    };
                    _service.AddPrescription(pr);
                    MessageBox.Show("New prescription added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // UPDATE
                    _currentPrescription.RecordId = recordId;
                    _currentPrescription.MedicationName = medName;
                    _currentPrescription.Dosage = dosage;
                    _currentPrescription.FrequencyId = frequencyId;
                    _currentPrescription.StartDate = startDate;
                    _currentPrescription.EndDate = endDate;
                    _currentPrescription.Instructions = instructions;
                    _service.UpdatePrescription(_currentPrescription);
                    MessageBox.Show("Prescription updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                RefreshPrescriptionGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving prescription: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// “Edit”: allow editing of selected prescription.
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPrescription != null)
                SetFormReadOnly(false);
        }

        /// <summary>
        /// “Delete”: confirm & delete prescription.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPrescription == null) return;

            var result = MessageBox.Show("Are you sure you want to delete this prescription?",
                                         "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _service.DeletePrescription(_currentPrescription.PrescriptionId);
                    MessageBox.Show("Prescription deleted.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshPrescriptionGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting prescription: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// “Refresh”: reload grid & clear form.
        /// </summary>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshPrescriptionGrid();
        }
    }
}
