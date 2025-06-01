// File: Views\TreatmentAssignmentView.xaml.cs
using MediTechDesktopApp.Models;
using MediTechDesktopApp.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediTechDesktopApp.Views
{
    public partial class TreatmentAssignmentView : UserControl
    {
        private TreatmentAssignmentViewModel _viewModel;

        public TreatmentAssignmentView()
        {
            InitializeComponent();
            _viewModel = (TreatmentAssignmentViewModel)this.DataContext;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Set placeholders
            TaPatientIdBox.Tag = "Patient ID";
            TaTreatmentIdBox.Tag = "Treatment ID";
            TaDoctorIdBox.Tag = "Doctor ID";
            TaNurseIdBox.Tag = "Nurse ID";
            TaNotesBox.Tag = "Notes";

            ResetPlaceholder(TaPatientIdBox, "Patient ID");
            ResetPlaceholder(TaTreatmentIdBox, "Treatment ID");
            ResetPlaceholder(TaDoctorIdBox, "Doctor ID");
            ResetPlaceholder(TaNurseIdBox, "Nurse ID");
            ResetPlaceholder(TaNotesBox, "Notes");

            // Default date
            TaDatePicker.SelectedDate = DateTime.Now;
        }

        private void AddAssignment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Parse integers or default to 0 if placeholders
                int pid = 0, tid = 0, did = 0, nid = 0;
                int.TryParse((TaPatientIdBox.Text == "Patient ID") ? "0" : TaPatientIdBox.Text.Trim(), out pid);
                int.TryParse((TaTreatmentIdBox.Text == "Treatment ID") ? "0" : TaTreatmentIdBox.Text.Trim(), out tid);
                int.TryParse((TaDoctorIdBox.Text == "Doctor ID") ? "0" : TaDoctorIdBox.Text.Trim(), out did);
                int.TryParse((TaNurseIdBox.Text == "Nurse ID") ? "0" : TaNurseIdBox.Text.Trim(), out nid);

                var dt = TaDatePicker.SelectedDate ?? DateTime.Now;
                var notes = (TaNotesBox.Text == "Notes") ? string.Empty : TaNotesBox.Text.Trim();

                var ta = new TreatmentAssignment
                {
                    PatientId = pid,
                    TreatmentId = tid,
                    AssignmentDate = dt,
                    AssignedDoctorId = did,
                    AssignedNurseId = nid,
                    Notes = notes
                };

                _viewModel.AddAssignment(ta);
                MessageBox.Show("Assignment added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset fields
                ResetPlaceholder(TaPatientIdBox, "Patient ID");
                ResetPlaceholder(TaTreatmentIdBox, "Treatment ID");
                ResetPlaceholder(TaDoctorIdBox, "Doctor ID");
                ResetPlaceholder(TaNurseIdBox, "Nurse ID");
                TaDatePicker.SelectedDate = DateTime.Now;
                ResetPlaceholder(TaNotesBox, "Notes");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding assignment: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Placeholder Helpers

        private void Placeholder_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                var placeholder = tb.Tag as string;
                if (placeholder != null && tb.Text == placeholder)
                {
                    tb.Text = string.Empty;
                    tb.Foreground = Brushes.Black;
                }
            }
        }

        private void Placeholder_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                var placeholder = tb.Tag as string;
                if (placeholder != null && string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.Text = placeholder;
                    tb.Foreground = Brushes.Gray;
                }
            }
        }

        private void ResetPlaceholder(TextBox tb, string placeholder)
        {
            tb.Text = placeholder;
            tb.Foreground = Brushes.Gray;
            tb.Tag = placeholder;
        }

        #endregion
    }
}
