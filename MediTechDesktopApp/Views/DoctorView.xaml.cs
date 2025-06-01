// File: Views\DoctorView.xaml.cs
using MediTechDesktopApp.Models;
using MediTechDesktopApp.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediTechDesktopApp.Views
{
    public partial class DoctorView : UserControl
    {
        private DoctorViewModel _viewModel;

        public DoctorView()
        {
            InitializeComponent();
            _viewModel = (DoctorViewModel)this.DataContext;
        }

        private void AddDoctor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // If placeholders are still present, treat them as empty
                var firstName      = (FirstNameBox.Text == "First Name")      ? string.Empty : FirstNameBox.Text.Trim();
                var lastName       = (LastNameBox.Text  == "Last Name")       ? string.Empty : LastNameBox.Text.Trim();
                var specialization = (SpecializationBox.Text == "Specialization") ? string.Empty : SpecializationBox.Text.Trim();
                var phone          = (PhoneBox.Text     == "Phone")           ? string.Empty : PhoneBox.Text.Trim();
                var email          = (EmailBox.Text     == "Email")           ? string.Empty : EmailBox.Text.Trim();

                var doctor = new Doctor
                {
                    FirstName      = firstName,
                    LastName       = lastName,
                    Specialization = specialization,
                    ContactPhone   = phone,
                    ContactEmail   = email
                };

                _viewModel.AddDoctor(doctor);

                MessageBox.Show("Doctor added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset placeholders:
                ResetPlaceholder(FirstNameBox,      "First Name");
                ResetPlaceholder(LastNameBox,       "Last Name");
                ResetPlaceholder(SpecializationBox, "Specialization");
                ResetPlaceholder(PhoneBox,          "Phone");
                ResetPlaceholder(EmailBox,          "Email");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding doctor: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            // Assign the Tag (placeholder) so Got/LostFocus can react
            FirstNameBox.Tag      = "First Name";
            LastNameBox.Tag       = "Last Name";
            SpecializationBox.Tag = "Specialization";
            PhoneBox.Tag          = "Phone";
            EmailBox.Tag          = "Email";
        }

        #endregion
    }
}
