// File: ViewModels\InsuranceProviderViewModel.cs
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;
using System.Collections.ObjectModel;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// VM to manage a list of InsuranceProvider, plus Add/Update/Delete.
    /// </summary>
    public class InsuranceProviderViewModel
    {
        private readonly InsuranceProviderService _service;

        public ObservableCollection<InsuranceProvider> Providers { get; private set; }
            = new ObservableCollection<InsuranceProvider>();

        public InsuranceProvider SelectedProvider { get; set; } = new InsuranceProvider();

        public InsuranceProviderViewModel()
        {
            _service = new InsuranceProviderService();
            LoadProviders();
        }

        public void LoadProviders()
        {
            Providers.Clear();
            var all = _service.GetAllProviders();
            foreach (var prov in all)
                Providers.Add(prov);
        }

        public void AddProvider(InsuranceProvider prov)
        {
            if (prov == null) return;
            _service.AddProvider(prov);
            LoadProviders();
        }

        public void UpdateProvider()
        {
            if (SelectedProvider == null || SelectedProvider.ProviderId <= 0) return;
            _service.UpdateProvider(
                SelectedProvider.ProviderId,
                SelectedProvider.ContactPhone,
                SelectedProvider.ContactEmail
            );
            LoadProviders();
        }

        public void DeleteProvider(int providerId)
        {
            if (providerId <= 0) return;
            _service.DeleteProvider(providerId);
            LoadProviders();
        }
    }
}
