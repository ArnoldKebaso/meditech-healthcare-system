// File: Views\NurseView.xaml.cs
using MediTechDesktopApp.Models;
using MediTechDesktopApp.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediTechDesktopApp.Views
{
    public partial class NurseView : UserControl
    {
        private NurseViewModel _viewModel;

        public NurseView()
        {
            InitializeComponent();
            _viewModel = (NurseViewModel)this.DataContext;
        }

        private void AddNurse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var firstName  = (FirstNameBox.Text == "First Name")  ? string.Empty : FirstNameBox.Text.Trim();
                var lastName   = (LastNameBox.Text  == "Last Name")   ? string.Empty : LastNameBox.Text.Trim();
                var department = (DeptBox.Text      == "Department")  ? string.Empty : DeptBox.Text.Trim();
                var phone      = (PhoneBox.Text     == "Phone")       ? string.Empty : PhoneBox.Text.Trim();
                var email      = (EmailBox.Text     == "Email")       ? string.Empty : EmailBox.Text.Trim();

                var nurse = new Nurse
                {
                    FirstName    = firstName,
                    LastName     = lastName,
                    Department   = department,
                    ContactPhone = phone,
                    ContactEmail = email
                };

                _viewModel.AddNurse(nurse);

                MessageBox.Show("Nurse added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset placeholders
                ResetPlaceholder(FirstNameBox, "First Name");
                ResetPlaceholder(LastNameBox,  "Last Name");
                ResetPlaceholder(DeptBox,      "Department");
                ResetPlaceholder(PhoneBox,     "Phone");
                ResetPlaceholder(EmailBox,     "Email");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding nurse: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Placeholder Logic

        private void Placeholder_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                var tagPlaceholder = tb.Tag as string;
                if (tagPlaceholder != null && tb.Text == tagPlaceholder)
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
                var tagPlaceholder = tb.Tag as string;
                if (tagPlaceholder != null && string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.Text = tagPlaceholder;
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
            LastNameBox.Tag  = "Last Name";
            DeptBox.Tag      = "Department";
            PhoneBox.Tag     = "Phone";
            EmailBox.Tag     = "Email";
        }

        #endregion
    }
}
