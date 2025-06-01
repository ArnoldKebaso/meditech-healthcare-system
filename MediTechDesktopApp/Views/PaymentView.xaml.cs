using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.ViewModels;

namespace MediTechDesktopApp.Views
{
    public partial class PaymentView : UserControl
    {
        private PaymentViewModel _viewModel;

        public PaymentView()
        {
            InitializeComponent();
            _viewModel = (PaymentViewModel)this.DataContext;
        }

        private void AddPayment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Parse Invoice ID
                if (!int.TryParse(InvoiceIdBox.Text, out int invoiceId))
                {
                    MessageBox.Show("Invalid Invoice ID", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Parse Amount Paid
                if (!decimal.TryParse(AmountPaidBox.Text, out decimal amt))
                {
                    MessageBox.Show("Invalid Amount Paid", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var pay = new Payment
                {
                    InvoiceId = invoiceId,
                    PaymentDate = PaymentDatePicker.SelectedDate ?? DateTime.Now,
                    AmountPaid = amt,
                    Method = ((ComboBoxItem)MethodCombo.SelectedItem)?.Content.ToString() ?? "Cash",
                    TransactionRef = TransactionRefBox.Text.Trim()
                };

                _viewModel.AddPayment(pay);

                MessageBox.Show("Payment added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset placeholders
                ResetPlaceholder(InvoiceIdBox, "Invoice ID");
                AmountPaidBox.Text = "Amount Paid";
                AmountPaidBox.Foreground = Brushes.Gray;
                MethodCombo.SelectedIndex = 0;
                TransactionRefBox.Text = "Transaction Ref";
                TransactionRefBox.Foreground = Brushes.Gray;
                PaymentDatePicker.SelectedDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding payment: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            InvoiceIdBox.Tag = "Invoice ID";
            AmountPaidBox.Tag = "Amount Paid";
            TransactionRefBox.Tag = "Transaction Ref";
        }

        #endregion
    }
}
