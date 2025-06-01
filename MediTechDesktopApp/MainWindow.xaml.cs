using System.Windows;
using MediTechDesktopApp.Views; // Make sure includes: PatientView, DoctorView, NurseView, AdminStaffView, TreatmentView, TreatmentAssignmentView, PatientFileView, InsuranceProviderView, PatientInsuranceView, InvoiceView, PaymentView, MedicalRecordView, PrescriptionView

namespace MediTechDesktopApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // By default, show the Patients page:
            ContentArea.Content = new PatientView();
        }

        // ─── EXISTING BUTTON HANDLERS ────────────────────────────────────────────────
        private void BtnPatients_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new PatientView();
        }

        private void BtnDoctors_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new DoctorView();
        }

        private void BtnNurses_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new NurseView();
        }

        private void BtnAdminStaff_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new AdminStaffView();
        }

        private void BtnTreatments_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new TreatmentView();
        }

        private void BtnAssignments_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new TreatmentAssignmentView();
        }

        private void BtnFiles_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new PatientFileView();
        }

        private void BtnProviders_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new InsuranceProviderView();
        }

        private void BtnInsurance_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new InsuranceProviderView();
        }

        private void BtnPatientInsurance_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new PatientInsuranceView();
        }

        // ─── NEW BUTTON HANDLERS FOR INVOICES / PAYMENTS / RECORDS / PRESCRIPTIONS ──
        private void BtnInvoices_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new InvoiceView();
        }

        private void BtnPayments_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new PaymentView();
        }

        private void BtnRecords_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new MedicalRecordView();
        }

        private void BtnPrescriptions_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new PrescriptionView();
        }
    }
}
