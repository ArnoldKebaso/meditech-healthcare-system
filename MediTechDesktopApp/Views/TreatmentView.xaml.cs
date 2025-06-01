// File: Views\TreatmentView.xaml.cs
using MediTechDesktopApp.Models;
using MediTechDesktopApp.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediTechDesktopApp.Views
{
    public partial class TreatmentView : UserControl
    {
        private TreatmentViewModel _viewModel;

        public TreatmentView()
        {
            InitializeComponent();
            _viewModel = (TreatmentViewModel)this.DataContext;
        }

        private void AddTreatment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var name = (NameBox.Text == "Name") ? string.Empty : NameBox.Text.Trim();
                var desc = (DescBox.Text == "Description") ? string.Empty : DescBox.Text.Trim();

                var treatment = new Treatment
                {
                    Name = name,
                    Description = desc
                };

                _viewModel.AddTreatment(treatment);
                MessageBox.Show("Treatment added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset placeholders
                ResetPlaceholder(NameBox, "Name");
                ResetPlaceholder(DescBox, "Description");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding treatment: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            NameBox.Tag = "Name";
            DescBox.Tag = "Description";
        }

        #endregion
    }
}
