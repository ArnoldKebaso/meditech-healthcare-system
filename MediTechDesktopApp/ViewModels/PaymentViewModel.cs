// File: ViewModels\PaymentViewModel.cs
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// ViewModel to expose Payment list and CRUD methods for PaymentView.xaml.
    /// </summary>
    public class PaymentViewModel
    {
        private readonly PaymentService _service;

        public ObservableCollection<Payment> Payments { get; private set; }
            = new ObservableCollection<Payment>();

        public Payment SelectedPayment { get; set; } = new Payment();

        public PaymentViewModel()
        {
            _service = new PaymentService();
            LoadPayments();
        }

        /// <summary>
        /// Loads all payments from DB into the ObservableCollection.
        /// </summary>
        public void LoadPayments()
        {
            Payments.Clear();
            var list = _service.GetAllPayments();
            foreach (var pay in list)
            {
                Payments.Add(pay);
            }
        }

        /// <summary>
        /// Adds a new payment and reloads the collection.
        /// </summary>
        public void AddPayment(Payment pay)
        {
            if (pay == null) return;
            _service.AddPayment(pay);
            LoadPayments();
        }

        /// <summary>
        /// Deletes payment by ID and reloads.
        /// </summary>
        public void DeletePayment(int paymentId)
        {
            _service.DeletePayment(paymentId);
            LoadPayments();
        }

        /// <summary>
        /// Returns newline list of Payment IDs (for quick testing).
        /// </summary>
        public string GetPaymentIds()
        {
            return string.Join("\n", Payments.Select(p => p.PaymentId));
        }
    }
}
