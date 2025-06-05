using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.Views
{
    public partial class PatientFileView : UserControl
    {
        private readonly PatientFileService _service;
        private List<PatientFile> _allFiles;
        private PatientFile _currentFile;
        private List<Patient> _allPatients;

        public PatientFileView()
        {
            InitializeComponent();
            _service = new PatientFileService();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadPatientsDropdown();
                RefreshGrid();
                SetFormReadOnly();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Patient File form: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadPatientsDropdown()
        {
            _allPatients = _service.GetAllPatients();
            cbPatients.ItemsSource = _allPatients;
            cbPatients.DisplayMemberPath = "FullName";
            cbPatients.SelectedValuePath = "PatientId";
        }

        private void RefreshGrid()
        {
            _allFiles = _service.GetAllPatientFiles();
            dgPatientFiles.ItemsSource = _allFiles;
            ClearForm();
            SetFormReadOnly();
            btnDelete.IsEnabled = false;
        }

        private void ClearForm()
        {
            cbPatients.SelectedIndex = -1;
            txtFileName.Text = "";
            txtFilePath.Text = "";
            _currentFile = null;
        }

        private void SetFormReadOnly()
        {
            cbPatients.IsEnabled = false;
            btnBrowse.IsEnabled = false;
            txtFileName.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnNew.IsEnabled = true;
            btnRefresh.IsEnabled = true;
        }

        private void dgPatientFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPatientFiles.SelectedItem is PatientFile selected)
            {
                _currentFile = selected;
                cbPatients.SelectedValue = selected.PatientId;
                txtFileName.Text = selected.FileName;
                txtFilePath.Text = ""; // not needed here
                btnDelete.IsEnabled = true;
            }
            else
            {
                btnDelete.IsEnabled = false;
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            cbPatients.IsEnabled = true;
            btnBrowse.IsEnabled = true;
            txtFileName.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnRefresh.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Title = "Select a file to upload",
                Filter = "All Files|*.*"
            };
            if (dlg.ShowDialog() == true)
            {
                txtFilePath.Text = dlg.FileName;
                txtFileName.Text = System.IO.Path.GetFileName(dlg.FileName);
                txtFileName.IsEnabled = false;
                btnSave.IsEnabled = true;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbPatients.SelectedValue == null || string.IsNullOrWhiteSpace(txtFilePath.Text))
                {
                    MessageBox.Show("Please select a patient and a file first.", "Validation", 
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var chosenPatientId = (int)cbPatients.SelectedValue;
                var fileBytes = File.ReadAllBytes(txtFilePath.Text);
                var newFile = new PatientFile
                {
                    PatientId = chosenPatientId,
                    FileName = txtFileName.Text,
                    FileType = System.IO.Path.GetExtension(txtFileName.Text).TrimStart('.'),
                    FileSizeBytes = fileBytes.Length,
                    FileData = fileBytes
                };

                _service.AddPatientFile(newFile);
                MessageBox.Show("File uploaded successfully.", "Success", 
                                MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentFile == null) return;

            var result = MessageBox.Show("Are you sure you want to delete this file?",
                                         "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _service.DeletePatientFile(_currentFile.FileId);
                    MessageBox.Show("File deleted.", "Success", 
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting file: {ex.Message}", "Error",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshGrid();
        }

        private void cbPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // no action needed until Save is clicked
        }
    }
}
