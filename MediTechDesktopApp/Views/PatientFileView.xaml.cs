// File: Views\PatientFileView.xaml.cs
using MediTechDesktopApp.ViewModels;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediTechDesktopApp.Views
{
    public partial class PatientFileView : UserControl
    {
        private PatientFileViewModel _viewModel;
        private string _selectedFilePath = string.Empty;

        public PatientFileView()
        {
            InitializeComponent();
            _viewModel = (PatientFileViewModel)this.DataContext;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PfPatientIdBox.Tag = "Patient ID";
            ResetPlaceholder(PfPatientIdBox, "Patient ID");

            SelectedFilePath.Text = "No file chosen";
            SelectedFilePath.Foreground = Brushes.Gray;
        }

        private void BrowseFile_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Title = "Select a file to upload",
                Filter = "All Files|*.*"
            };
            if (dlg.ShowDialog() == true)
            {
                _selectedFilePath = dlg.FileName;
                SelectedFilePath.Text = dlg.SafeFileName;
                SelectedFilePath.Foreground = Brushes.Black;
            }
        }

        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int pid = 0;
                int.TryParse((PfPatientIdBox.Text == "Patient ID") ? "0" : PfPatientIdBox.Text.Trim(), out pid);

                if (pid <= 0 || string.IsNullOrWhiteSpace(_selectedFilePath))
                {
                    MessageBox.Show("Please enter a valid Patient ID and select a file first.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _viewModel.AddFile(pid, _selectedFilePath);
                MessageBox.Show("File uploaded successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset both input fields
                ResetPlaceholder(PfPatientIdBox, "Patient ID");
                _selectedFilePath = string.Empty;
                SelectedFilePath.Text = "No file chosen";
                SelectedFilePath.Foreground = Brushes.Gray;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error uploading file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
