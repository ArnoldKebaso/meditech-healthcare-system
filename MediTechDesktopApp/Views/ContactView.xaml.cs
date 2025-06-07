using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace MediTechDesktopApp.Views
{
    public partial class ContactView : UserControl
    {
        public ContactView()
        {
            InitializeComponent();
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            // simple validation
            if (string.IsNullOrWhiteSpace(TxtName.Text) ||
                string.IsNullOrWhiteSpace(TxtEmail.Text) ||
                !Regex.IsMatch(TxtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") ||
                string.IsNullOrWhiteSpace(TxtSubject.Text) ||
                string.IsNullOrWhiteSpace(TxtMessage.Text))
            {
                MessageBox.Show(
                    "Please fill in all fields with valid information.",
                    "Validation Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            // Simulate sending...
            MessageBox.Show(
                $"Thank you, {TxtName.Text}! Your message has been sent.",
                "Message Sent",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            // clear form
            TxtName.Clear();
            TxtEmail.Clear();
            TxtSubject.Clear();
            TxtMessage.Clear();
        }
    }
}
