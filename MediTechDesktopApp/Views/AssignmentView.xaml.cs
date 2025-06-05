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
    /// Interaction logic for AssignmentView.xaml
    /// </summary>
    public partial class AssignmentView : UserControl
    {
        private readonly TreatmentAssignmentService _service;
        private readonly PatientService _patientService;
        private readonly TreatmentService _treatmentService;
        private readonly DoctorService _doctorService;
        private readonly NurseService _nurseService;

        private List<TreatmentAssignment> _allAssignments;
        private TreatmentAssignment _currentAssignment;

        public AssignmentView()
        {
            InitializeComponent();

            _service = new TreatmentAssignmentService();
            _patientService = new PatientService();
            _treatmentService = new TreatmentService();
            _doctorService = new DoctorService();
            _nurseService = new NurseService();
        }

        /// <summary>
        /// Loaded event: populate all 4 lookup comboBoxes and the grid.
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadLookups();
                RefreshAssignmentGrid();
                SetFormReadOnly(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Assignment form: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Load data for Patient, Treatment, Doctor, Nurse comboBoxes.
        /// </summary>
        private void LoadLookups()
        {
            // 1) Patients
            var patients = _patientService.GetAllPatients();
            cbPatient.ItemsSource = patients;
            cbPatient.SelectedIndex = -1;

            // 2) Treatments
            var treatments = _treatmentService.GetAllTreatments();
            cbTreatment.ItemsSource = treatments;
            cbTreatment.SelectedIndex = -1;

            // 3) Doctors
            var doctors = _doctorService.GetAllDoctors();
            cbDoctor.ItemsSource = doctors;
            cbDoctor.SelectedIndex = -1;

            // 4) Nurses
            var nurses = _nurseService.GetAllNurses();
            cbNurse.ItemsSource = nurses;
            cbNurse.SelectedIndex = -1;
        }

        /// <summary>
        /// Fetch all assignments and bind to grid. Then clear form.
        /// </summary>
        private void RefreshAssignmentGrid()
        {
            _allAssignments = _service.GetAllAssignments();
            dgAssignments.ItemsSource = _allAssignments;

            ClearForm();
            SetFormReadOnly(true);
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        /// <summary>
        /// Clears the form fields (comboBoxes, date, notes).
        /// </summary>
        private void ClearForm()
        {
            cbPatient.SelectedIndex = -1;
            cbTreatment.SelectedIndex = -1;
            cbDoctor.SelectedIndex = -1;
            cbNurse.SelectedIndex = -1;
            dpDate.SelectedDate = null;
            txtNotes.Text = "";
            _currentAssignment = null;
        }

        /// <summary>
        /// Enables or disables the form fields + Save button.
        /// </summary>
        private void SetFormReadOnly(bool isReadOnly)
        {
            cbPatient.IsEnabled = !isReadOnly;
            cbTreatment.IsEnabled = !isReadOnly;
            cbDoctor.IsEnabled = !isReadOnly;
            cbNurse.IsEnabled = !isReadOnly;
            dpDate.IsEnabled = !isReadOnly;
            txtNotes.IsEnabled = !isReadOnly;

            btnSave.IsEnabled = !isReadOnly;
            btnNew.IsEnabled = isReadOnly;
            btnRefresh.IsEnabled = isReadOnly;
        }

        /// <summary>
        /// Fired when a row in DataGrid is selected.
        /// Populates the form fields with that assignment.
        /// </summary>
        private void dgAssignments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAssignments.SelectedItem is TreatmentAssignment sel)
            {
                _currentAssignment = sel;

                // 1) Select correct Patient by PatientId
                for (int i = 0; i < cbPatient.Items.Count; i++)
                {
                    if (((Patient)cbPatient.Items[i]).PatientId == sel.PatientId)
                    {
                        cbPatient.SelectedIndex = i;
                        break;
                    }
                }

                // 2) Select correct Treatment by TreatmentId
                for (int i = 0; i < cbTreatment.Items.Count; i++)
                {
                    if (((Treatment)cbTreatment.Items[i]).TreatmentId == sel.TreatmentId)
                    {
                        cbTreatment.SelectedIndex = i;
                        break;
                    }
                }

                // 3) Select correct Doctor by DoctorId
                for (int i = 0; i < cbDoctor.Items.Count; i++)
                {
                    if (((Doctor)cbDoctor.Items[i]).DoctorId == sel.AssignedDoctorId)
                    {
                        cbDoctor.SelectedIndex = i;
                        break;
                    }
                }

                // 4) Select correct Nurse by NurseId
                for (int i = 0; i < cbNurse.Items.Count; i++)
                {
                    if (((Nurse)cbNurse.Items[i]).NurseId == sel.AssignedNurseId)
                    {
                        cbNurse.SelectedIndex = i;
                        break;
                    }
                }

                dpDate.SelectedDate = sel.AssignmentDate;
                txtNotes.Text = sel.Notes;

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
        /// “New” button: clear form, allow entry for a brand-new assignment.
        /// </summary>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            SetFormReadOnly(false);
            _currentAssignment = null;
        }

        /// <summary>
        /// “Save” button: validate all required fields, insert or update.
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1) Validate that user chose each combo + date
                if (cbPatient.SelectedItem == null ||
                    cbTreatment.SelectedItem == null ||
                    cbDoctor.SelectedItem == null ||
                    cbNurse.SelectedItem == null ||
                    dpDate.SelectedDate == null)
                {
                    MessageBox.Show("Patient, Treatment, Doctor, Nurse, and Date are required.",
                                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int pid = ((Patient)cbPatient.SelectedItem).PatientId;
                int tid = ((Treatment)cbTreatment.SelectedItem).TreatmentId;
                int did = ((Doctor)cbDoctor.SelectedItem).DoctorId;
                int nid = ((Nurse)cbNurse.SelectedItem).NurseId;
                DateTime dt = dpDate.SelectedDate.Value;
                string notes = txtNotes.Text.Trim();

                // 2) If currentAssignment==null → INSERT
                if (_currentAssignment == null)
                {
                    var newAsgmt = new TreatmentAssignment
                    {
                        PatientId = pid,
                        TreatmentId = tid,
                        AssignedDoctorId = did,
                        AssignedNurseId = nid,
                        AssignmentDate = dt,
                        Notes = notes
                    };

                    _service.AddAssignment(newAsgmt);
                    MessageBox.Show("New assignment added.",
                                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // 3) Otherwise → UPDATE only “Notes” in this example
                    _currentAssignment.Notes = notes;
                    _service.UpdateAssignment(_currentAssignment);
                    MessageBox.Show("Assignment updated.",
                                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                RefreshAssignmentGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving assignment: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// “Edit” button: allow editing of the selected assignment (Notes only).
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_currentAssignment != null)
                SetFormReadOnly(false);
        }

        /// <summary>
        /// “Delete” button: confirm and delete the selected assignment.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentAssignment == null) return;

            var result = MessageBox.Show(
                "Are you sure you want to delete this assignment?",
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _service.DeleteAssignment(
                        _currentAssignment.PatientId,
                        _currentAssignment.TreatmentId,
                        _currentAssignment.AssignmentDate);

                    MessageBox.Show("Assignment deleted.",
                                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshAssignmentGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting assignment: {ex.Message}",
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// “Refresh” button: reload grid + clear form.
        /// </summary>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshAssignmentGrid();
        }
    }
}
