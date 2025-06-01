// File: Views\InsuranceProviderView.xaml.cs
using MediTechDesktopApp.Models;
using MediTechDesktopApp.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediTechDesktopApp.Views
{
    public partial class InsuranceProviderView : UserControl
    {
        private InsuranceProviderViewModel _viewModel;

        public InsuranceProviderView()
        {
            InitializeComponent();
            _viewModel = (InsuranceProviderViewModel)this.DataContext;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ProvNameBox.Tag = "Provider Name";
            ProvPhoneBox.Tag = "Phone";
            ProvEmailBox.Tag = "Email";

            ResetPlaceholder(ProvNameBox, "Provider Name");
            ResetPlaceholder(ProvPhoneBox, "Phone");
            ResetPlaceholder(ProvEmailBox, "Email");
        }

        private void AddProvider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var name = (ProvNameBox.Text == "Provider Name") ? string.Empty : ProvNameBox.Text.Trim();
                var phone = (ProvPhoneBox.Text == "Phone") ? string.Empty : ProvPhoneBox.Text.Trim();
                var email = (ProvEmailBox.Text == "Email") ? string.Empty : ProvEmailBox.Text.Trim();

                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Provider Name cannot be empty.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var prov = new InsuranceProvider
                {
                    Name = name,
                    ContactPhone = phone,
                    ContactEmail = email
                };

                _viewModel.AddProvider(prov);
                MessageBox.Show("Provider added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset fields
                ResetPlaceholder(ProvNameBox, "Provider Name");
                ResetPlaceholder(ProvPhoneBox, "Phone");
                ResetPlaceholder(ProvEmailBox, "Email");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding provider: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
