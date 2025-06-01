using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.ViewModels;

namespace MediTechDesktopApp.Views
{
    public partial class MedicalRecordView : UserControl
    {
        private MedicalRecordViewModel _viewModel;

        public MedicalRecordView()
        {
            InitializeComponent();
            _viewModel = (MedicalRecordViewModel)this.DataContext;
        }

        private void AddRecord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Parse Appointment ID
                if (!int.TryParse(AppointmentIdBox.Text, out int apptId))
                {
                    MessageBox.Show("Invalid Appointment ID", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var rec = new MedicalRecord
                {
                    AppointmentId = apptId,
                    Diagnosis = DiagnosisBox.Text.Trim(),
                    VisitSummary = VisitSummaryBox.Text.Trim(),
                    DoctorNotes = DoctorNotesBox.Text.Trim()
                    // CreatedAt is set by DB default
                };

                _viewModel.AddRecord(rec);

                MessageBox.Show("Record added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset placeholders
                ResetPlaceholder(AppointmentIdBox, "Appointment ID");
                ResetPlaceholder(DiagnosisBox, "Diagnosis");
                ResetPlaceholder(VisitSummaryBox, "Visit Summary");
                ResetPlaceholder(DoctorNotesBox, "Doctor Notes");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding record: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Placeholder Logic

        private void Placeholder_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                var tag = tb.Tag as string;
                if (tag != null && tb.Text == tag)
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
                var tag = tb.Tag as string;
                if (tag != null && string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.Text = tag;
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
            AppointmentIdBox.Tag = "Appointment ID";
            DiagnosisBox.Tag = "Diagnosis";
            VisitSummaryBox.Tag = "Visit Summary";
            DoctorNotesBox.Tag = "Doctor Notes";
        }

        #endregion
    }
}
