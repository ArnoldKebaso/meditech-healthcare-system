using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.ViewModels;

namespace MediTechDesktopApp.Views
{
    public partial class PrescriptionView : UserControl
    {
        private PrescriptionViewModel _viewModel;

        public PrescriptionView()
        {
            InitializeComponent();
            _viewModel = (PrescriptionViewModel)this.DataContext;
        }

        private void AddPrescription_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Parse Record ID
                if (!int.TryParse(RecordIdBox.Text, out int recordId))
                {
                    MessageBox.Show("Invalid Record ID", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var pres = new Prescription
                {
                    RecordId = recordId,
                    MedicationName = MedicationNameBox.Text.Trim(),
                    Dosage = DosageBox.Text.Trim(),
                    Frequency = FrequencyBox.Text.Trim(),
                    StartDate = StartDatePicker.SelectedDate,
                    EndDate = EndDatePicker.SelectedDate,
                    Instructions = InstructionsBox.Text.Trim()
                };

                _viewModel.AddPrescription(pres);

                MessageBox.Show("Prescription added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset placeholders
                ResetPlaceholder(RecordIdBox, "Record ID");
                ResetPlaceholder(MedicationNameBox, "Medication");
                ResetPlaceholder(DosageBox, "Dosage");
                ResetPlaceholder(FrequencyBox, "Frequency");
                StartDatePicker.SelectedDate = DateTime.Now;
                EndDatePicker.SelectedDate = DateTime.Now;
                ResetPlaceholder(InstructionsBox, "Instructions");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding prescription: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            RecordIdBox.Tag = "Record ID";
            MedicationNameBox.Tag = "Medication";
            DosageBox.Tag = "Dosage";
            FrequencyBox.Tag = "Frequency";
            InstructionsBox.Tag = "Instructions";
            StartDatePicker.SelectedDate = DateTime.Now;
            EndDatePicker.SelectedDate = DateTime.Now;
        }

        #endregion
    }
}
