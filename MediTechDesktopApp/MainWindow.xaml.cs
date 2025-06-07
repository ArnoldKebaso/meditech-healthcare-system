using System.Linq;
using System.Windows;
using MediTechDesktopApp.Services;
using MediTechDesktopApp.Views;

namespace MediTechDesktopApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 1) Default to HomeView
            ContentArea.Content = new HomeView();

            // 2) Hide sidebar until successful login
            LeftNavPanel.Visibility = Visibility.Collapsed;
            BtnLogOut.Visibility = Visibility.Collapsed;
            BtnDashboard.Visibility = Visibility.Visible;
        }

        // ───── Top Bar ─────

        private void BtnHome_Click(object sender, RoutedEventArgs e)
            => ContentArea.Content = new HomeView();

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
            => ContentArea.Content = new AboutView();

        private void BtnContact_Click(object sender, RoutedEventArgs e)
            => ContentArea.Content = new ContactView();

        //private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        //{
        //    // Show login dialog
        //    var login = new LoginWindow();
        //    if (login.ShowDialog() == true)
        //    {
        //        // Reveal sidebar
        //        LeftNavPanel.Visibility = Visibility.Visible;
        //        BtnLogOut.Visibility = Visibility.Visible;
        //        BtnDashboard.Visibility = Visibility.Collapsed;

        //        // Default content after login
        //        ContentArea.Content = new PatientView();
        //        RefreshNavigationState();
        //    }
        //}
        public void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            var login = new LoginWindow();
            if (login.ShowDialog() == true)
            {
                LeftNavPanel.Visibility = Visibility.Visible;
                BtnLogOut.Visibility = Visibility.Visible;
                BtnDashboard.Visibility = Visibility.Collapsed;

                ContentArea.Content = new PatientView();
                RefreshNavigationState();
            }
        }
        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            // Hide sidebar, go back to Home
            LeftNavPanel.Visibility = Visibility.Collapsed;
            BtnLogOut.Visibility = Visibility.Collapsed;
            BtnDashboard.Visibility = Visibility.Visible;
            ContentArea.Content = new HomeView();
        }

        // ───── Sidebar Navigation ─────

        private void BtnDoctors_Click(object sender, RoutedEventArgs e)
            => ContentArea.Content = new DoctorView();

        private void BtnNurses_Click(object sender, RoutedEventArgs e)
            => ContentArea.Content = new NurseView();

        private void BtnAdminStaff_Click(object sender, RoutedEventArgs e)
            => ContentArea.Content = new AdminStaffView();

        private void BtnPatients_Click(object sender, RoutedEventArgs e)
            => ContentArea.Content = new PatientView();

        private void BtnPatientFiles_Click(object sender, RoutedEventArgs e)
            => ContentArea.Content = new PatientFileView();

        private void BtnPtInsurance_Click(object sender, RoutedEventArgs e)
            => ContentArea.Content = new PatientInsuranceView();

        private void BtnAppointments_Click(object sender, RoutedEventArgs e)
            => ContentArea.Content = new AppointmentView();

        private void BtnMedRecords_Click(object sender, RoutedEventArgs e)
            => ContentArea.Content = new MedicalRecordView();

        private void BtnPrescriptions_Click(object sender, RoutedEventArgs e)
            => ContentArea.Content = new PrescriptionView();

        private void BtnTreatments_Click(object sender, RoutedEventArgs e)
            => ContentArea.Content = new TreatmentView();

        private void BtnAssignments_Click(object sender, RoutedEventArgs e)
            => ContentArea.Content = new AssignmentView();

        private void BtnInsProviders_Click(object sender, RoutedEventArgs e)
            => ContentArea.Content = new InsuranceProviderView();

        private void BtnInsPolicies_Click(object sender, RoutedEventArgs e)
            => ContentArea.Content = new InsurancePolicyView();

        private void BtnInvoices_Click(object sender, RoutedEventArgs e)
            => ContentArea.Content = new InvoiceView();

        private void BtnPayments_Click(object sender, RoutedEventArgs e)
            => ContentArea.Content = new PaymentView();

        private void BtnDepartments_Click(object sender, RoutedEventArgs e)
            => ContentArea.Content = new DepartmentView();

        /// <summary>
        /// Enables/disables nav buttons based on whether there are any records
        /// in the corresponding tables. Call this after any CRUD change.
        /// </summary>
        private void RefreshNavigationState()
        {
            // PATIENT
            bool hasPatients = new PatientService().GetAllPatients().Any();
            BtnPatientFiles.IsEnabled = hasPatients;
            BtnPtInsurance.IsEnabled = hasPatients;

            // STAFF
            bool hasDoctors = new DoctorService().GetAllDoctors().Any();
            bool hasNurses = new NurseService().GetAllNurses().Any();
            bool hasAdmin = new AdminStaffService().GetAllAdmins().Any();
            BtnDoctors.IsEnabled = hasDoctors;
            BtnNurses.IsEnabled = hasNurses;
            BtnAdminStaff.IsEnabled = hasAdmin;

            // CLINICAL
            bool hasAppointments = new AppointmentService().GetAllAppointments().Any();
            bool hasMedRecords = new MedicalRecordService().GetAllMedicalRecords().Any();
            bool hasTreatments = new TreatmentService().GetAllTreatments().Any();
            BtnAppointments.IsEnabled = hasPatients;
            BtnMedRecords.IsEnabled = hasAppointments;
            BtnPrescriptions.IsEnabled = hasMedRecords;
            BtnTreatments.IsEnabled = hasTreatments;
            BtnAssignments.IsEnabled = hasPatients && hasTreatments;

            // BILLING & INSURANCE
            bool hasProviders = new InsuranceProviderService().GetAllProviders().Any();
            bool hasPolicies = new InsurancePolicyService().GetAllPolicies().Any();
            bool hasInvoices = new InvoiceService().GetAllInvoices().Any();
            BtnInsProviders.IsEnabled = hasProviders;
            BtnInsPolicies.IsEnabled = hasProviders;
            BtnInvoices.IsEnabled = hasPolicies;
            BtnPayments.IsEnabled = hasInvoices;

            // DEPARTMENTS (always enabled)
            BtnDepartments.IsEnabled = true;
        }
    }
}
