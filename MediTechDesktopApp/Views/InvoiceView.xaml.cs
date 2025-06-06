// Views/InvoiceView.xaml.cs
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
    /// Interaction logic for InvoiceView.xaml
    /// </summary>
    public partial class InvoiceView : UserControl
    {
        private readonly InvoiceService _service;
        private List<Invoice> _allInvoices;
        private Invoice _currentInvoice = null;

        public InvoiceView()
        {
            InitializeComponent();
            _service = new InvoiceService();
        }

        /// <summary>
        /// On load, populate ComboBoxes and DataGrid.
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadComboBoxes();
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Invoice form: {ex.Message}",
                                "Initialization Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Populate cbPatient with (id, fullName). cbStatus is static.
        /// </summary>
        private void LoadComboBoxes()
        {
            var patients = _service.GetAllPatientsForCombo();
            cbPatient.ItemsSource = patients;
            cbPatient.DisplayMemberPath = "Name";
            cbPatient.SelectedValuePath = "Id";

            // cbStatus items are defined in XAML; no need to re‐load here.
        }

        /// <summary>
        /// Fetch all invoices and bind to DataGrid. Clear form afterward.
        /// </summary>
        private void LoadGrid()
        {
            _allInvoices = _service.GetAllInvoices();
            dgInvoices.ItemsSource = _allInvoices;

            ClearForm();
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = false;
        }

        /// <summary>
        /// Resets form controls and local state.
        /// </summary>
        private void ClearForm()
        {
            cbPatient.SelectedIndex = -1;
            dpInvoiceDate.SelectedDate = null;
            txtTotalAmount.Text = string.Empty;
            cbStatus.SelectedIndex = -1;

            _currentInvoice = null;
            btnNew.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        /// <summary>
        /// When a user picks a row in the DataGrid, fill the form fields.
        /// </summary>
        private void dgInvoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgInvoices.SelectedItem is Invoice inv)
            {
                _currentInvoice = inv;
                cbPatient.SelectedValue = inv.PatientId;
                dpInvoiceDate.SelectedDate = inv.InvoiceDate;
                txtTotalAmount.Text = inv.TotalAmount.ToString("F2");
                cbStatus.SelectedItem = cbStatus.Items.OfType<ComboBoxItem>()
                                       .FirstOrDefault(i => i.Content.ToString() == inv.Status);

                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnSave.IsEnabled = false;
            }
            else
            {
                btnUpdate.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnSave.IsEnabled = false;
            }
        }

        /// <summary>
        /// “New” clears the form for a fresh insert.
        /// </summary>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            btnSave.IsEnabled = true; // allow Save now
        }

        /// <summary>
        /// “Save” inserts a new invoice (no InvoiceId yet).
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate Patient selection
                if (cbPatient.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a Patient.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validate Invoice Date
                if (!dpInvoiceDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please choose an Invoice Date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validate Total Amount
                if (!decimal.TryParse(txtTotalAmount.Text.Trim(), out decimal amount) || amount < 0)
                {
                    MessageBox.Show("Please enter a valid positive amount.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validate Status selection
                if (cbStatus.SelectedItem == null)
                {
                    MessageBox.Show("Please select a Status (Pending, Paid, or Overdue).", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newInv = new Invoice
                {
                    PatientId = (int)cbPatient.SelectedValue,
                    InvoiceDate = dpInvoiceDate.SelectedDate.Value,
                    TotalAmount = amount,
                    Status = ((ComboBoxItem)cbStatus.SelectedItem).Content.ToString()
                };

                _service.AddInvoice(newInv);
                MessageBox.Show("Invoice successfully added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving Invoice: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// “Update” modifies the selected invoice’s date, amount, and status.
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_currentInvoice == null) return;

            try
            {
                // Validate fields as above
                if (cbPatient.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a Patient.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!dpInvoiceDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please choose an Invoice Date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!decimal.TryParse(txtTotalAmount.Text.Trim(), out decimal amount) || amount < 0)
                {
                    MessageBox.Show("Please enter a valid positive amount.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (cbStatus.SelectedItem == null)
                {
                    MessageBox.Show("Please select a Status (Pending, Paid, or Overdue).", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Update properties on the local Invoice object
                _currentInvoice.PatientId = (int)cbPatient.SelectedValue;
                _currentInvoice.InvoiceDate = dpInvoiceDate.SelectedDate.Value;
                _currentInvoice.TotalAmount = amount;
                _currentInvoice.Status = ((ComboBoxItem)cbStatus.SelectedItem).Content.ToString();

                _service.UpdateInvoice(_currentInvoice);
                MessageBox.Show("Invoice successfully updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating Invoice: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// “Delete” removes the selected invoice.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentInvoice == null) return;

            var result = MessageBox.Show("Are you sure you want to delete this invoice?", "Confirm Delete",
                                          MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _service.DeleteInvoice(_currentInvoice.InvoiceId);
                    MessageBox.Show("Invoice successfully deleted.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting Invoice: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// “Refresh” reloads data from the database.
        /// </summary>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadComboBoxes();
            LoadGrid();
        }
    }
}
