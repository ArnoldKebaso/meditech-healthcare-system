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
    /// Interaction logic for AppointmentView.xaml
    /// </summary>
    public partial class AppointmentView : UserControl
    {
        private readonly AppointmentService _service;
        private List<Appointment> _appointments;
        private Appointment _currentAppointment = null;

        public AppointmentView()
        {
            InitializeComponent();
            _service = new AppointmentService();
        }

        /// <summary>
        /// Loaded event: populates DataGrid and ComboBoxes.
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshAppointmentGrid();
                SetFormReadOnly(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Appointment form: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Loads all appointments and binds DataGrid; sets up ComboBoxes.
        /// </summary>
        private void RefreshAppointmentGrid()
        {
            // 1) Fetch appointments
            _appointments = _service.GetAllAppointments();
            dgAppointments.ItemsSource = _appointments;

            // 2) Populate Patient & Doctor comboBoxes
            cbPatients.ItemsSource = _service.GetAllPatientsForCombo()
                                        .Select(x => new ComboBoxItem
                                        {
                                            Content = x.name,
                                            Tag = x.id
                                        }).ToList();
            cbDoctors.ItemsSource = _service.GetAllDoctorsForCombo()
                                        .Select(x => new ComboBoxItem
                                        {
                                            Content = x.name,
                                            Tag = x.id
                                        }).ToList();

            // 3) Clear form fields & disable editing
            ClearFormFields();
            SetFormReadOnly(true);
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        /// <summary>
        /// Clears all form inputs and resets currentAppointment.
        /// </summary>
        private void ClearFormFields()
        {
            cbPatients.SelectedIndex = -1;
            cbDoctors.SelectedIndex = -1;
            dpAppointmentDate.SelectedDate = null;
            cbStatus.SelectedIndex = -1;
            txtNotes.Text = "";
            _currentAppointment = null;
        }

        /// <summary>
        /// Sets IsEnabled = false/true on inputs and Save/New buttons.
        /// </summary>
        private void SetFormReadOnly(bool isReadOnly)
        {
            cbPatients.IsEnabled = !isReadOnly;
            cbDoctors.IsEnabled = !isReadOnly;
            dpAppointmentDate.IsEnabled = !isReadOnly;
            cbStatus.IsEnabled = !isReadOnly;
            txtNotes.IsEnabled = !isReadOnly;

            btnSave.IsEnabled = !isReadOnly;
            btnNew.IsEnabled = isReadOnly;
            btnRefresh.IsEnabled = isReadOnly;
        }

        /// <summary>
        /// SelectionChanged: populate form with selected row.
        /// </summary>
        private void dgAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAppointments.SelectedItem is Appointment sel)
            {
                _currentAppointment = sel;

                // Fill form fields
                // Select the correct ComboBoxItem by Tag
                foreach (ComboBoxItem item in cbPatients.Items)
                {
                    if ((int)item.Tag == sel.PatientId)
                    {
                        cbPatients.SelectedItem = item;
                        break;
                    }
                }
                foreach (ComboBoxItem item in cbDoctors.Items)
                {
                    if ((int)item.Tag == sel.DoctorId)
                    {
                        cbDoctors.SelectedItem = item;
                        break;
                    }
                }
                dpAppointmentDate.SelectedDate = sel.AppointmentDate;
                cbStatus.Text = sel.Status;
                txtNotes.Text = sel.Notes;

                // Enable Edit/Delete
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
        /// “New”: clear form & allow entry for a brand-new appointment.
        /// </summary>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearFormFields();
            SetFormReadOnly(false);
            _currentAppointment = null;
        }

        /// <summary>
        /// “Save”: either Insert (if new) or Update (if existing).
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1) Gather and validate inputs
                if (cbPatients.SelectedItem == null ||
                    cbDoctors.SelectedItem == null ||
                    dpAppointmentDate.SelectedDate == null ||
                    cbStatus.SelectedItem == null)
                {
                    MessageBox.Show("You must select a Patient, Doctor, pick a Date, and choose a Status.",
                                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int patientId = (int)((ComboBoxItem)cbPatients.SelectedItem).Tag;
                int doctorId = (int)((ComboBoxItem)cbDoctors.SelectedItem).Tag;
                DateTime apptDate = dpAppointmentDate.SelectedDate.Value;
                string status = ((ComboBoxItem)cbStatus.SelectedItem).Content.ToString();
                string notes = txtNotes.Text.Trim();

                if (_currentAppointment == null)
                {
                    // INSERT
                    var a = new Appointment
                    {
                        PatientId = patientId,
                        DoctorId = doctorId,
                        AppointmentDate = apptDate,
                        Status = status,
                        Notes = notes
                    };
                    _service.AddAppointment(a);
                    MessageBox.Show("New appointment added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // UPDATE
                    _currentAppointment.PatientId = patientId;
                    _currentAppointment.DoctorId = doctorId;
                    _currentAppointment.AppointmentDate = apptDate;
                    _currentAppointment.Status = status;
                    _currentAppointment.Notes = notes;
                    _service.UpdateAppointment(_currentAppointment);
                    MessageBox.Show("Appointment updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                RefreshAppointmentGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving appointment: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// “Edit”: allow editing of selected appointment.
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_currentAppointment != null)
                SetFormReadOnly(false);
        }

        /// <summary>
        /// “Delete”: confirm & delete the selected appointment.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentAppointment == null) return;

            var result = MessageBox.Show("Are you sure you want to delete this appointment?",
                                         "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _service.DeleteAppointment(_currentAppointment.AppointmentId);
                    MessageBox.Show("Appointment deleted.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshAppointmentGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting appointment: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// “Refresh”: reload grid & clear form.
        /// </summary>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshAppointmentGrid();
        }
    }
}
