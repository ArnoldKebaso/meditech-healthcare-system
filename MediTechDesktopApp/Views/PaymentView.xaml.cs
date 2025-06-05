// File: Views/PaymentView.xaml.cs
using System;
using System.Collections.Generic;
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
        private List<Payment> _rows;
        private Payment _currentPayment;

        // For dropdown:
        private List<(int Id, string Label)> _allInvoices;

        public PaymentView()
        {
            InitializeComponent();
            _service = new PaymentService();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1) Populate Invoice dropdown:
                _allInvoices = _service.GetAllInvoicesForCombo();
                cbInvoices.ItemsSource = _allInvoices;
                cbInvoices.SelectedIndex = -1;

                // 2) Load grid:
                RefreshGrid();

                // Initially disable Save/Edit/Delete:
                btnSave.IsEnabled = false;
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Payment form: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshGrid()
        {
            _rows = _service.GetAllPayments();
            dgPayments.ItemsSource = _rows;

            // Clear form:
            _currentPayment = null;
            txtPaymentId.Text = "";
            cbInvoices.SelectedIndex = -1;
            dpPaymentDate.SelectedDate = null;
            txtAmountPaid.Text = "";
            cbMethod.SelectedIndex = -1;
            txtTransactionRef.Text = "";

            btnSave.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        private void dgPayments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPayments.SelectedItem is Payment sel)
            {
                _currentPayment = sel;
                txtPaymentId.Text = sel.PaymentId.ToString();
                // Find invoice dropdown by matching Id:
                cbInvoices.SelectedValue = sel.InvoiceId;
                dpPaymentDate.SelectedDate = sel.PaymentDate;
                txtAmountPaid.Text = sel.AmountPaid.ToString("F2");
                // Method:
                foreach (ComboBoxItem item in cbMethod.Items)
                {
                    if ((string)item.Content == sel.Method)
                    {
                        cbMethod.SelectedItem = item;
                        break;
                    }
                }
                txtTransactionRef.Text = sel.TransactionRef;

                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnSave.IsEnabled = false; // Save only after Edit
            }
            else
            {
                _currentPayment = null;
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnSave.IsEnabled = false;
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            dgPayments.UnselectAll();
            _currentPayment = null;

            txtPaymentId.Text = "";
            cbInvoices.SelectedIndex = -1;
            dpPaymentDate.SelectedDate = DateTime.Today;
            txtAmountPaid.Text = "";
            cbMethod.SelectedIndex = 0; // default “Cash”
            txtTransactionRef.Text = "";

            btnSave.IsEnabled = true;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPayment != null)
            {
                btnSave.IsEnabled = true; // allow update
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbInvoices.SelectedValue == null ||
                    dpPaymentDate.SelectedDate == null ||
                    string.IsNullOrWhiteSpace(txtAmountPaid.Text) ||
                    cbMethod.SelectedItem == null)
                {
                    MessageBox.Show("You must select an Invoice, pick a Date, enter Amount Paid, and choose a Method.",
                                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int invoiceId = (int)cbInvoices.SelectedValue;
                DateTime payDate = dpPaymentDate.SelectedDate.Value;
                if (!decimal.TryParse(txtAmountPaid.Text.Trim(), out decimal amtPaid))
                {
                    MessageBox.Show("Amount Paid must be a valid decimal.", "Validation Error",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string method = ((ComboBoxItem)cbMethod.SelectedItem).Content.ToString();
                string txRef = txtTransactionRef.Text.Trim();

                if (_currentPayment == null)
                {
                    // ADD new:
                    _service.AddPayment(invoiceId, payDate, amtPaid, method, txRef);
                    MessageBox.Show("New payment added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // UPDATE existing:
                    _service.UpdatePayment(_currentPayment.PaymentId, amtPaid, method, txRef);
                    MessageBox.Show("Payment updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving payment: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPayment == null) return;

            var r = MessageBox.Show("Are you sure you want to delete this payment?",
                                     "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (r == MessageBoxResult.Yes)
            {
                try
                {
                    _service.DeletePayment(_currentPayment.PaymentId);
                    MessageBox.Show("Payment deleted.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting payment: {ex.Message}", "Error",
                                      MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshGrid();
        }
    }
}
