// File: Views\AppointmentView.xaml.cs
using MediTechDesktopApp.Models;
using MediTechDesktopApp.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediTechDesktopApp.Views
{
    public partial class AppointmentView : UserControl
    {
        private AppointmentViewModel _viewModel;

        public AppointmentView()
        {
            InitializeComponent();
            _viewModel = (AppointmentViewModel)this.DataContext;
        }

        private void AddAppointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Parse Patient ID and Doctor ID from textboxes (if placeholder present, treat as 0)
                int patientId = 0;
                int.TryParse(
                    (PatientIdBox.Text == "Patient ID") ? "0" : PatientIdBox.Text.Trim(),
                    out patientId);

                int doctorId = 0;
                int.TryParse(
                    (DoctorIdBox.Text == "Doctor ID") ? "0" : DoctorIdBox.Text.Trim(),
                    out doctorId);

                var apptDate = DatePickerAppt.SelectedDate ?? DateTime.Now;

                string status = ((ComboBoxItem)StatusCombo.SelectedItem)?.Content.ToString() ?? "Pending";

                var notes = (NotesBox.Text == "Notes") ? string.Empty : NotesBox.Text.Trim();

                var appt = new Appointment
                {
                    PatientId = patientId,
                    DoctorId = doctorId,
                    AppointmentDate = apptDate,
                    Status = status,
                    Notes = notes
                };

                _viewModel.AddAppointment(appt);

                MessageBox.Show("Appointment added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset placeholders and defaults
                ResetPlaceholder(PatientIdBox, "Patient ID");
                ResetPlaceholder(DoctorIdBox, "Doctor ID");
                DatePickerAppt.SelectedDate = DateTime.Now;
                StatusCombo.SelectedIndex = 0;
                ResetPlaceholder(NotesBox, "Notes");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding appointment: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Placeholder Logic

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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PatientIdBox.Tag = "Patient ID";
            DoctorIdBox.Tag = "Doctor ID";
            NotesBox.Tag = "Notes";
        }

        #endregion
    }
}
