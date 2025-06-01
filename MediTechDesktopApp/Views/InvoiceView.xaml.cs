using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.ViewModels;

namespace MediTechDesktopApp.Views
{
    public partial class InvoiceView : UserControl
    {
        private InvoiceViewModel _viewModel;

        public InvoiceView()
        {
            InitializeComponent();
            _viewModel = (InvoiceViewModel)this.DataContext;
        }

        private void AddInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Parse Patient ID
                if (!int.TryParse(PatientIdBox.Text, out int patientId))
                {
                    MessageBox.Show("Invalid Patient ID", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Parse Amount
                if (!decimal.TryParse(TotalAmountBox.Text, out decimal amount))
                {
                    MessageBox.Show("Invalid Amount", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var inv = new Invoice
                {
                    PatientId = patientId,
                    InvoiceDate = InvoiceDatePicker.SelectedDate ?? DateTime.Now,
                    TotalAmount = amount,
                    Status = ((ComboBoxItem)StatusCombo.SelectedItem)?.Content.ToString() ?? "Pending"
                };

                _viewModel.AddInvoice(inv);

                MessageBox.Show("Invoice added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset placeholders
                ResetPlaceholder(PatientIdBox, "Patient ID");
                TotalAmountBox.Text = "Amount";
                TotalAmountBox.Foreground = Brushes.Gray;
                StatusCombo.SelectedIndex = 0;
                InvoiceDatePicker.SelectedDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding invoice: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            PatientIdBox.Tag = "Patient ID";
            TotalAmountBox.Tag = "Amount";
        }

        #endregion
    }
}
