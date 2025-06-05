using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.Views
{
    public partial class InsurancePolicyView : UserControl
    {
        private readonly InsurancePolicyService _service;
        private readonly InsuranceProviderService _provService;
        private List<InsurancePolicy> _allPolicies;
        private List<InsuranceProvider> _allProviders;
        private InsurancePolicy _currentPolicy;

        public InsurancePolicyView()
        {
            InitializeComponent();
            _service = new InsurancePolicyService();
            _provService = new InsuranceProviderService();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadProvidersDropdown();
                RefreshGrid();
                SetFormReadOnly();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Policy form: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadProvidersDropdown()
        {
            _allProviders = _provService.GetAllProviders();
            cbProviders.ItemsSource = _allProviders;
            cbProviders.DisplayMemberPath = "Name";
            cbProviders.SelectedValuePath = "ProviderId";
        }

        private void RefreshGrid()
        {
            _allPolicies = _service.GetAllPolicies();
            dgPolicies.ItemsSource = _allPolicies;
            ClearForm();
            SetFormReadOnly();
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        private void ClearForm()
        {
            cbProviders.SelectedIndex = -1;
            txtPolicyNumber.Text = "";
            txtCoverage.Text = "";
            dpStartDate.SelectedDate = null;
            dpEndDate.SelectedDate = null;
            _currentPolicy = null;
        }

        private void SetFormReadOnly()
        {
            cbProviders.IsEnabled = false;
            txtPolicyNumber.IsEnabled = false;
            txtCoverage.IsEnabled = false;
            dpStartDate.IsEnabled = false;
            dpEndDate.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnNew.IsEnabled = true;
            btnRefresh.IsEnabled = true;
        }

        private void dgPolicies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPolicies.SelectedItem is InsurancePolicy selected)
            {
                _currentPolicy = selected;
                cbProviders.SelectedValue = selected.ProviderId;
                txtPolicyNumber.Text = selected.PolicyNumber;
                txtCoverage.Text = selected.CoverageDetails;
                dpStartDate.SelectedDate = selected.StartDate;
                dpEndDate.SelectedDate = selected.EndDate;
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
            cbProviders.IsEnabled = true;
            txtPolicyNumber.IsEnabled = true;
            txtCoverage.IsEnabled = true;
            dpStartDate.IsEnabled = true;
            dpEndDate.IsEnabled = true;
            btnSave.IsEnabled = true;
            btnRefresh.IsEnabled = false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbProviders.SelectedValue == null)
                {
                    MessageBox.Show("Please select an insurance provider.", "Validation",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int provId = (int)cbProviders.SelectedValue;
                string policyNum = txtPolicyNumber.Text.Trim();
                string coverage = txtCoverage.Text.Trim();
                DateTime? start = dpStartDate.SelectedDate;
                DateTime? end = dpEndDate.SelectedDate;

                if (string.IsNullOrWhiteSpace(policyNum) || start == null || end == null)
                {
                    MessageBox.Show("Policy Number, Start Date, and End Date are required.", "Validation",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_currentPolicy == null)
                {
                    var newPol = new InsurancePolicy
                    {
                        ProviderId = provId,
                        PolicyNumber = policyNum,
                        CoverageDetails = coverage,
                        StartDate = start.Value,
                        EndDate = end.Value
                    };
                    _service.AddPolicy(newPol);
                    MessageBox.Show("Policy added.", "Success",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _currentPolicy.ProviderId = provId;
                    _currentPolicy.PolicyNumber = policyNum;
                    _currentPolicy.CoverageDetails = coverage;
                    _currentPolicy.StartDate = start.Value;
                    _currentPolicy.EndDate = end.Value;
                    _service.UpdatePolicy(_currentPolicy);
                    MessageBox.Show("Policy updated.", "Success",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                }

                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving policy: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPolicy != null)
            {
                cbProviders.IsEnabled = true;
                txtPolicyNumber.IsEnabled = true;
                txtCoverage.IsEnabled = true;
                dpStartDate.IsEnabled = true;
                dpEndDate.IsEnabled = true;
                btnSave.IsEnabled = true;
                btnRefresh.IsEnabled = false;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPolicy == null) return;

            var result = MessageBox.Show("Delete this policy?", "Confirm Delete",
                                         MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _service.DeletePolicy(_currentPolicy.PolicyId);
                    MessageBox.Show("Policy deleted.", "Success",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting policy: {ex.Message}", "Error",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshGrid();
        }

        private void cbProviders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // nothing until Save is clicked
        }
    }
}
