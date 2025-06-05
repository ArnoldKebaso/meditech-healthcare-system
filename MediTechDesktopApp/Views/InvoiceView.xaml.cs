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
        /// On load, fill DataGrid + ComboBoxes.
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadComboBoxes();
                RefreshGrid();
                SetFormReadOnly(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Invoice form: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Populate patient list for cbPatient.
        /// </summary>
        private void LoadComboBoxes()
        {
            var patients = _service.GetAllPatientsForCombo();
            cbPatient.ItemsSource = patients;
            cbPatient.DisplayMemberPath = "fullName";
            cbPatient.SelectedValuePath = "id";
        }

        /// <summary>
        /// Reload grid from DB and clear form.
        /// </summary>
        private void RefreshGrid()
        {
            _allInvoices = _service.GetAllInvoices();
            dgInvoices.ItemsSource = _allInvoices;
            ClearForm();
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = false;
        }

        /// <summary>
        /// Clears form fields and resets current.
        /// </summary>
        private void ClearForm()
        {
            cbPatient.SelectedIndex = -1;
            dpInvoiceDate.SelectedDate = null;
            txtTotalAmount.Text = "";
            cbStatus.SelectedIndex = -1;
            _currentInvoice = null;
            btnSave.IsEnabled = false;
        }

        /// <summary>
        /// Puts form into read‐only state (except New & Refresh).
        /// </summary>
        private void SetFormReadOnly(bool isReadOnly)
        {
            cbPatient.IsEnabled = !isReadOnly;
            dpInvoiceDate.IsEnabled = !isReadOnly;
            txtTotalAmount.IsEnabled = !isReadOnly;
            cbStatus.IsEnabled = !isReadOnly;

            btnSave.IsEnabled = !isReadOnly;
            btnNew.IsEnabled = isReadOnly;
            btnRefresh.IsEnabled = isReadOnly;
        }

        /// <summary>
        /// When user selects a row, populate form fields.
        /// </summary>
        private void dgInvoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgInvoices.SelectedItem is Invoice inv)
            {
                _currentInvoice = inv;

                cbPatient.SelectedValue = inv.PatientId;
                dpInvoiceDate.SelectedDate = inv.InvoiceDate;
                txtTotalAmount.Text = inv.TotalAmount.ToString("F2");
                cbStatus.Text = inv.Status;

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
        /// “New” – clear form & allow editing.
        /// </summary>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            SetFormReadOnly(false);
        }

        /// <summary>
        /// “Save” – either Insert (if new) or Update (if existing).
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validation
                if (cbPatient.SelectedIndex < 0
                    || dpInvoiceDate.SelectedDate == null
                    || string.IsNullOrWhiteSpace(txtTotalAmount.Text)
                    || cbStatus.SelectedIndex < 0)
                {
                    MessageBox.Show("You must fill in Patient, Date, Amount, and Status.",
                                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int patientId = (int)cbPatient.SelectedValue;
                DateTime invDate = dpInvoiceDate.SelectedDate.Value;
                if (!decimal.TryParse(txtTotalAmount.Text.Trim(), out decimal amount))
                {
                    MessageBox.Show("Total Amount must be a valid decimal number.",
                                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                string status = ((ComboBoxItem)cbStatus.SelectedItem).Content.ToString();

                if (_currentInvoice == null)
                {
                    // INSERT
                    var model = new Invoice
                    {
                        PatientId = patientId,
                        InvoiceDate = invDate,
                        TotalAmount = amount,
                        Status = status
                    };
                    _service.AddInvoice(model);
                    MessageBox.Show("New invoice added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // UPDATE
                    _currentInvoice.PatientId = patientId;
                    _currentInvoice.InvoiceDate = invDate;
                    _currentInvoice.TotalAmount = amount;
                    _currentInvoice.Status = status;
                    _service.UpdateInvoice(_currentInvoice);
                    MessageBox.Show("Invoice updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                RefreshGrid();
                SetFormReadOnly(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving invoice: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// “Edit” – unlock fields (but keep current selection).
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_currentInvoice != null)
                SetFormReadOnly(false);
        }

        /// <summary>
        /// “Delete” – remove the selected invoice.
        /// Note: payments referencing this will cause FK error if exist.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentInvoice == null) return;

            var result = MessageBox.Show("Are you sure you want to delete this invoice? " +
                                         "All associated payments must be deleted first, or you will get an FK error.",
                                         "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _service.DeleteInvoice(_currentInvoice.InvoiceId);
                    MessageBox.Show("Invoice deleted.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting invoice: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// “Refresh” – reload data.
        /// </summary>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadComboBoxes();
            RefreshGrid();
            SetFormReadOnly(true);
        }
    }
}
