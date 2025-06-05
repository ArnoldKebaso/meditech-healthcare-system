using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.Views
{
    public partial class InsuranceProviderView : UserControl
    {
        private readonly InsuranceProviderService _service;
        private List<InsuranceProvider> _allProviders;
        private InsuranceProvider _currentProvider;

        public InsuranceProviderView()
        {
            InitializeComponent();
            _service = new InsuranceProviderService();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshGrid();
                SetFormReadOnly();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Provider form: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshGrid()
        {
            _allProviders = _service.GetAllProviders();
            dgProviders.ItemsSource = _allProviders;
            ClearForm();
            SetFormReadOnly();
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        private void ClearForm()
        {
            txtProviderName.Text = "";
            txtProviderPhone.Text = "";
            txtProviderEmail.Text = "";
            _currentProvider = null;
        }

        private void SetFormReadOnly()
        {
            txtProviderName.IsEnabled = false;
            txtProviderPhone.IsEnabled = false;
            txtProviderEmail.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnNew.IsEnabled = true;
            btnRefresh.IsEnabled = true;
        }

        private void dgProviders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgProviders.SelectedItem is InsuranceProvider selected)
            {
                _currentProvider = selected;
                txtProviderName.Text = selected.Name;
                txtProviderPhone.Text = selected.ContactPhone;
                txtProviderEmail.Text = selected.ContactEmail;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
            else
            {
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            txtProviderName.IsEnabled = true;
            txtProviderPhone.IsEnabled = true;
            txtProviderEmail.IsEnabled = true;
            btnSave.IsEnabled = true;
            btnRefresh.IsEnabled = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtProviderName.Text.Trim();
                string phone = txtProviderPhone.Text.Trim();
                string email = txtProviderEmail.Text.Trim();

                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Name is required.", "Validation",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_currentProvider == null)
                {
                    var newProv = new InsuranceProvider
                    {
                        Name = name,
                        ContactPhone = phone,
                        ContactEmail = email
                    };
                    _service.AddProvider(newProv);
                    MessageBox.Show("Provider added.", "Success",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _currentProvider.Name = name;
                    _currentProvider.ContactPhone = phone;
                    _currentProvider.ContactEmail = email;
                    _service.UpdateProvider(_currentProvider);
                    MessageBox.Show("Provider updated.", "Success",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                }

                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving provider: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_currentProvider != null)
            {
                txtProviderName.IsEnabled = true;
                txtProviderPhone.IsEnabled = true;
                txtProviderEmail.IsEnabled = true;
                btnSave.IsEnabled = true;
                btnRefresh.IsEnabled = false;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentProvider == null) return;

            var result = MessageBox.Show("Delete this provider?", "Confirm Delete",
                                         MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _service.DeleteProvider(_currentProvider.ProviderId);
                    MessageBox.Show("Provider deleted.", "Success",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting provider: {ex.Message}", "Error",
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
