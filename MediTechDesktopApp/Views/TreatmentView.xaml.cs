using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;
using MediTechDesktopApp.ViewModels; // Add this namespace

namespace MediTechDesktopApp.Views { 
    public partial class TreatmentView : UserControl
    {
        private readonly TreatmentService _service;
        private List<Treatment> _allTreatments;
        private Treatment _currentTreatment;

        public TreatmentView()
        {
            InitializeComponent();
            _service = new TreatmentService();
        }

        /// <summary>
        /// Loaded event: fetch all treatments into grid.
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshTreatmentGrid();
                SetFormReadOnly(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Treatment form: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Loads all treatments from DB and binds DataGrid.
        /// </summary>
        private void RefreshTreatmentGrid()
        {
            _allTreatments = _service.GetAllTreatments();
            dgTreatments.ItemsSource = _allTreatments;

            ClearForm();
            SetFormReadOnly(true);
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        /// <summary>
        /// Clears form fields and resets currentTreatment.
        /// </summary>
        private void ClearForm()
        {
            txtName.Text = "";
            txtDescription.Text = "";
            _currentTreatment = null;
        }

        /// <summary>
        /// Enables/disables input fields.
        /// </summary>
        private void SetFormReadOnly(bool isReadOnly)
        {
            txtName.IsEnabled = !isReadOnly;
            txtDescription.IsEnabled = !isReadOnly;

            btnSave.IsEnabled = !isReadOnly;
            btnNew.IsEnabled = isReadOnly;
            btnRefresh.IsEnabled = isReadOnly;
        }

        /// <summary>
        /// Fired when user selects a row in the grid.
        /// Populates form fields.
        /// </summary>
        private void dgTreatments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgTreatments.SelectedItem is Treatment selected)
            {
                _currentTreatment = selected;
                txtName.Text = selected.Name;
                txtDescription.Text = selected.Description;

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
        /// “New” button: clear form, allow entry for a brand-new treatment.
        /// </summary>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            SetFormReadOnly(false);
            _currentTreatment = null;
        }

        /// <summary>
        /// “Save” button: validate and insert/update accordingly.
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtName.Text.Trim();
                string desc = txtDescription.Text.Trim();

                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Name is required.",
                                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_currentTreatment == null)
                {
                    // INSERT
                    var t = new Treatment
                    {
                        Name = name,
                        Description = desc
                    };
                    _service.AddTreatment(t);
                    MessageBox.Show("New treatment added.",
                                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // UPDATE
                    _currentTreatment.Name = name;
                    _currentTreatment.Description = desc;
                    _service.UpdateTreatment(_currentTreatment);
                    MessageBox.Show("Treatment updated.",
                                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                RefreshTreatmentGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving treatment: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// “Edit” button: allow editing of selected treatment.
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_currentTreatment != null)
                SetFormReadOnly(false);
        }

        /// <summary>
        /// “Delete” button: confirm and delete the selected treatment.
        /// (If it is referenced by other tables, you will get a FK error.)
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentTreatment == null) return;

            var result = MessageBox.Show(
                "Are you sure you want to delete this treatment? " +
                "If it is referenced by existing assignments, it cannot be deleted.",
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _service.DeleteTreatment(_currentTreatment.TreatmentId);
                    MessageBox.Show("Treatment deleted.",
                                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshTreatmentGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting treatment: {ex.Message}",
                                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// “Refresh” button: reload grid and clear form.
        /// </summary>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshTreatmentGrid();
        }
    }
}
