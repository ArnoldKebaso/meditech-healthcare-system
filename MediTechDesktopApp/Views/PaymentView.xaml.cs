// Views/PaymentView.xaml.cs
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
    /// Interaction logic for PaymentView.xaml
    /// </summary>
    public partial class PaymentView : UserControl
    {
        private readonly PaymentService _service;
        private List<Payment> _allPayments;
        private Payment _currentPayment = null;

        public PaymentView()
        {
            InitializeComponent();
            _service = new PaymentService();
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
                MessageBox.Show($"Error initializing Payment form: {ex.Message}",
                                "Initialization Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Populate cbInvoice with (id, invoiceDisplay). cbMethod is static.
        /// </summary>
        private void LoadComboBoxes()
        {
            var invoices = _service.GetAllInvoicesForCombo();
            cbInvoice.ItemsSource = invoices;
            cbInvoice.DisplayMemberPath = "Name";    // ComboItem.Name holds "Inv # – Patient"
            cbInvoice.SelectedValuePath = "Id";      // ComboItem.Id holds invoice_id

            // cbMethod items are defined in XAML; no need to reload here.
        }

        /// <summary>
        /// Fetch all payments and bind to DataGrid. Clear form afterward.
        /// </summary>
        private void LoadGrid()
        {
            _allPayments = _service.GetAllPayments();
            dgPayments.ItemsSource = _allPayments;

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
            cbInvoice.SelectedIndex = -1;
            dpPaymentDate.SelectedDate = null;
            txtAmountPaid.Text = string.Empty;
            cbMethod.SelectedIndex = -1;

            _currentPayment = null;
            btnNew.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        /// <summary>
        /// When a user picks a row in the DataGrid, fill the form fields.
        /// </summary>
        private void dgPayments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPayments.SelectedItem is Payment pay)
            {
                _currentPayment = pay;
                cbInvoice.SelectedValue = pay.InvoiceId;
                dpPaymentDate.SelectedDate = pay.PaymentDate;
                txtAmountPaid.Text = pay.AmountPaid.ToString("F2");
                cbMethod.SelectedItem = cbMethod.Items.OfType<ComboBoxItem>()
                                          .FirstOrDefault(i => i.Content.ToString() == pay.Method);

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
        /// “Save” inserts a new payment.
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate Invoice selection
                if (cbInvoice.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select an Invoice.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validate Payment Date
                if (!dpPaymentDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please choose a Payment Date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validate Amount Paid
                if (!decimal.TryParse(txtAmountPaid.Text.Trim(), out decimal amount) || amount < 0)
                {
                    MessageBox.Show("Please enter a valid positive amount.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validate Method selection
                if (cbMethod.SelectedItem == null)
                {
                    MessageBox.Show("Please select a Payment Method (Cash, Credit Card, Insurance, or Other).",
                                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newPayment = new Payment
                {
                    InvoiceId = (int)cbInvoice.SelectedValue,
                    PaymentDate = dpPaymentDate.SelectedDate.Value,
                    AmountPaid = amount,
                    Method = ((ComboBoxItem)cbMethod.SelectedItem).Content.ToString()
                };

                _service.AddPayment(newPayment);
                MessageBox.Show("Payment successfully added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving Payment: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// “Update” modifies the selected payment’s date, amount, and method.
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPayment == null) return;

            try
            {
                // Validate fields as above
                if (cbInvoice.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select an Invoice.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!dpPaymentDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please choose a Payment Date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (!decimal.TryParse(txtAmountPaid.Text.Trim(), out decimal amount) || amount < 0)
                {
                    MessageBox.Show("Please enter a valid positive amount.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (cbMethod.SelectedItem == null)
                {
                    MessageBox.Show("Please select a Payment Method (Cash, Credit Card, Insurance, or Other).",
                                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Update properties on the local Payment object
                _currentPayment.InvoiceId = (int)cbInvoice.SelectedValue;
                _currentPayment.PaymentDate = dpPaymentDate.SelectedDate.Value;
                _currentPayment.AmountPaid = amount;
                _currentPayment.Method = ((ComboBoxItem)cbMethod.SelectedItem).Content.ToString();

                _service.UpdatePayment(_currentPayment);
                MessageBox.Show("Payment successfully updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating Payment: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// “Delete” removes the selected payment.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPayment == null) return;

            var result = MessageBox.Show("Are you sure you want to delete this payment?", "Confirm Delete",
                                          MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _service.DeletePayment(_currentPayment.PaymentId);
                    MessageBox.Show("Payment successfully deleted.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting Payment: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
