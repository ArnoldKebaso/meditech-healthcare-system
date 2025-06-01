// File: Views\PatientInsuranceView.xaml.cs
using MediTechDesktopApp.Models;
using MediTechDesktopApp.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediTechDesktopApp.Views
{
    public partial class PatientInsuranceView : UserControl
    {
        private PatientInsuranceViewModel _viewModel;

        public PatientInsuranceView()
        {
            InitializeComponent();
            _viewModel = (PatientInsuranceViewModel)this.DataContext;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PiPatientIdBox.Tag = "Patient ID";
            PiProviderIdBox.Tag = "Provider ID";
            PiPolicyBox.Tag = "Policy #";
            PiCoverageBox.Tag = "Coverage Details";

            ResetPlaceholder(PiPatientIdBox, "Patient ID");
            ResetPlaceholder(PiProviderIdBox, "Provider ID");
            ResetPlaceholder(PiPolicyBox, "Policy #");
            ResetPlaceholder(PiCoverageBox, "Coverage Details");

            PiStartDatePicker.SelectedDate = DateTime.Now;
            PiEndDatePicker.SelectedDate = DateTime.Now;
        }

        private void AddInsurance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int pid = 0, prid = 0;
                int.TryParse((PiPatientIdBox.Text == "Patient ID") ? "0" : PiPatientIdBox.Text.Trim(), out pid);
                int.TryParse((PiProviderIdBox.Text == "Provider ID") ? "0" : PiProviderIdBox.Text.Trim(), out prid);

                var pol = (PiPolicyBox.Text == "Policy #") ? string.Empty : PiPolicyBox.Text.Trim();
                var cov = (PiCoverageBox.Text == "Coverage Details") ? string.Empty : PiCoverageBox.Text.Trim();
                var sDate = PiStartDatePicker.SelectedDate;
                var eDate = PiEndDatePicker.SelectedDate;

                if (pid <= 0 || prid <= 0 || string.IsNullOrWhiteSpace(pol))
                {
                    MessageBox.Show("Patient ID, Provider ID, and Policy # are required.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var pi = new PatientInsurance
                {
                    PatientId = pid,
                    ProviderId = prid,
                    PolicyNumber = pol,
                    CoverageDetails = cov,
                    StartDate = sDate,
                    EndDate = eDate
                };

                _viewModel.AddInsurance(pi);
                MessageBox.Show("Insurance record added!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset fields
                ResetPlaceholder(PiPatientIdBox, "Patient ID");
                ResetPlaceholder(PiProviderIdBox, "Provider ID");
                ResetPlaceholder(PiPolicyBox, "Policy #");
                ResetPlaceholder(PiCoverageBox, "Coverage Details");
                PiStartDatePicker.SelectedDate = DateTime.Now;
                PiEndDatePicker.SelectedDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding insurance: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateInsurance_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedInsurance == null)
            {
                MessageBox.Show("Please select an insurance record to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                _viewModel.SelectedInsurance.EndDate = PiEndDatePicker.SelectedDate;
                _viewModel.SelectedInsurance.CoverageDetails = PiCoverageBox.Text != "Coverage Details"
                    ? PiCoverageBox.Text.Trim()
                    : string.Empty;

                _viewModel.UpdateInsurance();
                MessageBox.Show("Insurance record updated!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                _viewModel.LoadInsurances();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating insurance: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteInsurance_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedInsurance == null)
            {
                MessageBox.Show("Please select an insurance record to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var ans = MessageBox.Show(
                    "Are you sure you want to delete this insurance record?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (ans == MessageBoxResult.Yes)
                {
                    _viewModel.DeleteInsurance();
                    MessageBox.Show("Insurance record deleted!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting insurance: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
