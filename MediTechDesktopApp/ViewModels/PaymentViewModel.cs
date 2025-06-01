using System.Collections.ObjectModel;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// ViewModel to handle Payment CRUD and expose an ObservableCollection of Payment.
    /// </summary>
    public class PaymentViewModel
    {
        private readonly PaymentService _service;

        /// <summary>
        /// Collection bound to PaymentView’s DataGrid.
        /// </summary>
        public ObservableCollection<Payment> Payments { get; } = new ObservableCollection<Payment>();

        public Payment SelectedPayment { get; set; } = new Payment();

        public PaymentViewModel()
        {
            _service = new PaymentService();
            LoadPayments();
        }

        /// <summary>
        /// Reloads all payments from the DB.
        /// </summary>
        public void LoadPayments()
        {
            Payments.Clear();
            var all = _service.GetAllPayments();
            foreach (var p in all)
            {
                Payments.Add(p);
            }
        }

        /// <summary>
        /// Inserts a new payment via the Service and reloads list.
        /// </summary>
        public void AddPayment(Payment newPay)
        {
            if (newPay == null) return;
            _service.AddPayment(newPay);
            LoadPayments();
        }

        /// <summary>
        /// Deletes the specified payment.
        /// </summary>
        public void DeletePayment(int paymentId)
        {
            _service.DeletePayment(paymentId);
            LoadPayments();
        }
    }
}
