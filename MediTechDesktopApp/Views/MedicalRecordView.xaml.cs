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
    /// Interaction logic for MedicalRecordView.xaml
    /// </summary>
    public partial class MedicalRecordView : UserControl
    {
        private readonly MedicalRecordService _service;
        private List<MedicalRecord> _records;
        private MedicalRecord _currentRecord = null;

        public MedicalRecordView()
        {
            InitializeComponent();
            _service = new MedicalRecordService();
        }

        /// <summary>
        /// Loaded event: populates DataGrid and ComboBox.
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshMedicalRecordGrid();
                SetFormReadOnly(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Medical Record form: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Loads all records; sets up appointment ComboBox.
        /// </summary>
        private void RefreshMedicalRecordGrid()
        {
            // 1) Fetch records
            _records = _service.GetAllMedicalRecords();
            dgMedicalRecords.ItemsSource = _records;

            // 2) Populate Appointments dropdown
            cbAppointments.ItemsSource = _service.GetAllAppointmentsForCombo()
                                          .Select(x => new ComboBoxItem
                                          {
                                              Content = x.display,
                                              Tag = x.id
                                          }).ToList();

            // 3) Clear form & disable editing
            ClearFormFields();
            SetFormReadOnly(true);
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        /// <summary>
        /// Clears all input controls.
        /// </summary>
        private void ClearFormFields()
        {
            cbAppointments.SelectedIndex = -1;
            txtDiagnosis.Text = "";
            txtVisitSummary.Text = "";
            txtDoctorNotes.Text = "";
            _currentRecord = null;
        }

        /// <summary>
        /// Enable/disable form fields.
        /// </summary>
        private void SetFormReadOnly(bool isReadOnly)
        {
            cbAppointments.IsEnabled = !isReadOnly;
            txtDiagnosis.IsEnabled = !isReadOnly;
            txtVisitSummary.IsEnabled = !isReadOnly;
            txtDoctorNotes.IsEnabled = !isReadOnly;

            btnSave.IsEnabled = !isReadOnly;
            btnNew.IsEnabled = isReadOnly;
            btnRefresh.IsEnabled = isReadOnly;
        }

        /// <summary>
        /// When user selects a row, fill the form.
        /// </summary>
        private void dgMedicalRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgMedicalRecords.SelectedItem is MedicalRecord sel)
            {
                _currentRecord = sel;

                // Select correct ComboBoxItem by Tag
                foreach (ComboBoxItem item in cbAppointments.Items)
                {
                    if ((int)item.Tag == sel.AppointmentId)
                    {
                        cbAppointments.SelectedItem = item;
                        break;
                    }
                }
                txtDiagnosis.Text = sel.Diagnosis;
                txtVisitSummary.Text = sel.VisitSummary;
                txtDoctorNotes.Text = sel.DoctorNotes;

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
            _currentRecord = null;
        }

        /// <summary>
        /// “Save”: Insert or Update record.
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1) Validate inputs
                if (cbAppointments.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(txtDiagnosis.Text))
                {
                    MessageBox.Show("You must select an Appointment and enter a Diagnosis.",
                                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int appointmentId = (int)((ComboBoxItem)cbAppointments.SelectedItem).Tag;
                string diagnosis = txtDiagnosis.Text.Trim();
                string visitSummary = txtVisitSummary.Text.Trim();
                string doctorNotes = txtDoctorNotes.Text.Trim();

                if (_currentRecord == null)
                {
                    // INSERT
                    var mr = new MedicalRecord
                    {
                        AppointmentId = appointmentId,
                        Diagnosis = diagnosis,
                        VisitSummary = visitSummary,
                        DoctorNotes = doctorNotes
                    };
                    _service.AddMedicalRecord(mr);
                    MessageBox.Show("New medical record added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // UPDATE
                    _currentRecord.AppointmentId = appointmentId;
                    _currentRecord.Diagnosis = diagnosis;
                    _currentRecord.VisitSummary = visitSummary;
                    _currentRecord.DoctorNotes = doctorNotes;
                    _service.UpdateMedicalRecord(_currentRecord);
                    MessageBox.Show("Medical record updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                RefreshMedicalRecordGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving medical record: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// “Edit”: allow editing.
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_currentRecord != null)
                SetFormReadOnly(false);
        }

        /// <summary>
        /// “Delete”: confirm & delete the record.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentRecord == null) return;

            var result = MessageBox.Show("Are you sure you want to delete this medical record?",
                                         "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _service.DeleteMedicalRecord(_currentRecord.RecordId);
                    MessageBox.Show("Medical record deleted.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshMedicalRecordGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting record: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// “Refresh”: reload grid & clear form.
        /// </summary>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshMedicalRecordGrid();
        }
    }
}
