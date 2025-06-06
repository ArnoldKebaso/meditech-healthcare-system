using System.Linq;
using System.Windows;
using MediTechDesktopApp.Views;
using MediTechDesktopApp.Services;  // ensures all your *Service classes are in scope

namespace MediTechDesktopApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Start at Home
            ContentArea.Content = new HomeView();

            // Hide all Dashboard nav & show only the top “Dashboard” button until login
            LeftNavPanel.Visibility = Visibility.Collapsed;
            BtnLogOut.Visibility = Visibility.Collapsed;
            BtnDashboard.Visibility = Visibility.Visible;
        }

        // ───────── Top Bar Handlers ─────────

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new HomeView();
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new AboutView();
        }

        private void BtnContact_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new ContactView();
        }

        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            // Launch a modal LoginWindow; assume it returns true on successful login.
            var loginWindow = new LoginWindow();
            bool? result = loginWindow.ShowDialog();

            if (result == true)
            {
                // Unhide navigation
                LeftNavPanel.Visibility = Visibility.Visible;
                BtnLogOut.Visibility = Visibility.Visible;
                BtnDashboard.Visibility = Visibility.Collapsed;

                // Show Patients by default
                ContentArea.Content = new PatientView();
                RefreshNavigationState();
            }
        }

        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            // Hide navigation, go to Home
            LeftNavPanel.Visibility = Visibility.Collapsed;
            BtnLogOut.Visibility = Visibility.Collapsed;
            BtnDashboard.Visibility = Visibility.Visible;

            ContentArea.Content = new HomeView();
        }

        // ───────── Left Nav Handlers ─────────

        private void BtnPatients_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new PatientView();
        }

        private void BtnPatientFiles_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new PatientFileView();
        }

        private void BtnPtInsurance_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new PatientInsuranceView();
        }

        private void BtnAppointments_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new AppointmentView();
        }

        private void BtnMedRecords_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new MedicalRecordView();
        }

        private void BtnPrescriptions_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new PrescriptionView();
        }

        private void BtnTreatments_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new TreatmentView();
        }

        private void BtnAssignments_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new AssignmentView();
        }

        private void BtnInsProviders_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new InsuranceProviderView();
        }

        private void BtnInsPolicies_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new InsurancePolicyView();
        }

        private void BtnInvoices_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new InvoiceView();
        }

        private void BtnPayments_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new PaymentView();
        }

        private void BtnDepartments_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new DepartmentView();
        }

        /// <summary>
        /// Dynamically enables or disables secondary navigation buttons
        /// based on whether related records exist in the database.
        /// Call this whenever you add or remove key records.
        /// </summary>
        private void RefreshNavigationState()
        {
            // PATIENT block
            bool hasPatients = (new PatientService()).GetAllPatients().Any();
            BtnPatientFiles.IsEnabled = hasPatients;
            BtnPtInsurance.IsEnabled = hasPatients;

            // CLINICAL block
            bool hasAppointments = (new AppointmentService()).GetAllAppointments().Any();
            BtnMedRecords.IsEnabled = hasAppointments;

            bool hasMedRecords = (new MedicalRecordService()).GetAllMedicalRecords().Any();
            BtnPrescriptions.IsEnabled = hasMedRecords;

            // Optionally check Treatments/Assignments dependencies if needed
            // e.g. only enable Assignments if both a patient and a treatment exist

            // BILLING block
            bool hasProviders = (new InsuranceProviderService()).GetAllProviders().Any();
            BtnInsPolicies.IsEnabled = hasProviders;

            bool hasInvoices = (new InvoiceService()).GetAllInvoices().Any();
            BtnPayments.IsEnabled = hasInvoices;

            // Departments is standalone; you may leave it always enabled or put logic here
        }
    }
}
