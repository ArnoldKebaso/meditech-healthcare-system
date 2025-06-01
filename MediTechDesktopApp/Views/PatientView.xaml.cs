// File: Views\PatientView.xaml.cs
using MediTechDesktopApp.Models;
using MediTechDesktopApp.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediTechDesktopApp.Views
{
    public partial class PatientView : UserControl
    {
        private PatientViewModel _viewModel;

        public PatientView()
        {
            InitializeComponent();
            _viewModel = (PatientViewModel)this.DataContext;
        }

        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // If any placeholder text is still present, treat that as empty
                var firstName = (FirstNameBox.Text == "First Name") ? string.Empty : FirstNameBox.Text.Trim();
                var lastName  = (LastNameBox.Text  == "Last Name")  ? string.Empty : LastNameBox.Text.Trim();
                var phone     = (PhoneBox.Text     == "Phone")      ? string.Empty : PhoneBox.Text.Trim();
                var email     = (EmailBox.Text     == "Email")      ? string.Empty : EmailBox.Text.Trim();
                var address   = (AddressBox.Text   == "Address")    ? string.Empty : AddressBox.Text.Trim();

                var patient = new Patient
                {
                    FirstName    = firstName,
                    LastName     = lastName,
                    DateOfBirth  = DobPicker.SelectedDate ?? DateTime.Now,
                    Gender       = ((ComboBoxItem)GenderCombo.SelectedItem)?.Content.ToString() ?? "Other",
                    ContactPhone = phone,
                    ContactEmail = email,
                    Address      = address
                };

                _viewModel.AddPatient(patient);

                MessageBox.Show("Patient added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset placeholders:
                ResetPlaceholder(FirstNameBox, "First Name");
                ResetPlaceholder(LastNameBox,  "Last Name");
                ResetPlaceholder(PhoneBox,     "Phone");
                ResetPlaceholder(EmailBox,     "Email");
                ResetPlaceholder(AddressBox,   "Address");
                DobPicker.SelectedDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding patient: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Placeholder Logic

        private void Placeholder_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                // If the text is exactly the placeholder, clear it and set to black
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

        /// <summary>
        /// Resets a single TextBox to show its placeholder text in gray.
        /// </summary>
        private void ResetPlaceholder(TextBox tb, string placeholder)
        {
            tb.Text = placeholder;
            tb.Foreground = Brushes.Gray;
            tb.Tag = placeholder;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // When the control loads, set each Tag = placeholder for subsequent checks
            FirstNameBox.Tag = "First Name";
            LastNameBox.Tag  = "Last Name";
            PhoneBox.Tag     = "Phone";
            EmailBox.Tag     = "Email";
            AddressBox.Tag   = "Address";
        }

        #endregion
    }
}
