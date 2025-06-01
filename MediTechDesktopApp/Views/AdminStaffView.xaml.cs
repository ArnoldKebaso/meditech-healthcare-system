// File: Views\AdminStaffView.xaml.cs
using MediTechDesktopApp.Models;
using MediTechDesktopApp.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediTechDesktopApp.Views
{
    public partial class AdminStaffView : UserControl
    {
        private AdminStaffViewModel _viewModel;

        public AdminStaffView()
        {
            InitializeComponent();
            _viewModel = (AdminStaffViewModel)this.DataContext;
        }

        private void AddAdmin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var firstName = (FirstNameBox.Text == "First Name") ? string.Empty : FirstNameBox.Text.Trim();
                var lastName = (LastNameBox.Text == "Last Name") ? string.Empty : LastNameBox.Text.Trim();
                var role = (RoleBox.Text == "Role") ? string.Empty : RoleBox.Text.Trim();
                var phone = (PhoneBox.Text == "Phone") ? string.Empty : PhoneBox.Text.Trim();
                var email = (EmailBox.Text == "Email") ? string.Empty : EmailBox.Text.Trim();

                var admin = new AdminStaff
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Role = role,
                    ContactPhone = phone,
                    ContactEmail = email
                };

                _viewModel.AddAdmin(admin);
                MessageBox.Show("Admin Staff added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset placeholders
                ResetPlaceholder(FirstNameBox, "First Name");
                ResetPlaceholder(LastNameBox, "Last Name");
                ResetPlaceholder(RoleBox, "Role");
                ResetPlaceholder(PhoneBox, "Phone");
                ResetPlaceholder(EmailBox, "Email");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding admin staff: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Placeholder Logic

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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FirstNameBox.Tag = "First Name";
            LastNameBox.Tag = "Last Name";
            RoleBox.Tag = "Role";
            PhoneBox.Tag = "Phone";
            EmailBox.Tag = "Email";
        }

        #endregion
    }
}
