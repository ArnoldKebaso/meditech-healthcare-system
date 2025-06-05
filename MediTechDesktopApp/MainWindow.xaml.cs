// File: MainWindow.xaml.cs
using System.Windows;
using MediTechDesktopApp.Views;   // ← Make sure all your Views live under this namespace

namespace MediTechDesktopApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 1) On startup, load HomeView (“landing page”).
            ContentArea.Content = new HomeView();

            // 2) Ensure Dashboard nav is hidden; LogOut is hidden; Dashboard button visible.
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

        /// <summary>
        /// “Dashboard” button: always visible. Pops up LoginWindow.
        /// If login succeeds, reveal LeftNavPanel and LogOut, hide Dashboard button.
        /// </summary>
        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            bool? loginResult = loginWindow.ShowDialog();

            if (loginResult == true)
            {
                // User logged in successfully
                LeftNavPanel.Visibility = Visibility.Visible;
                BtnLogOut.Visibility = Visibility.Visible;
                BtnDashboard.Visibility = Visibility.Collapsed;

                // Optionally default to PatientsView after login:
                ContentArea.Content = new PatientView();
            }
            // else: login cancelled or failed → do nothing (stay on Home/Contact/whatever).
        }

        /// <summary>
        /// “Log Out” button: hides LeftNavPanel, shows Home, shows Dashboard button again.
        /// Does NOT close the application.
        /// </summary>
        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            LeftNavPanel.Visibility = Visibility.Collapsed;
            BtnLogOut.Visibility = Visibility.Collapsed;
            BtnDashboard.Visibility = Visibility.Visible;

            ContentArea.Content = new HomeView();
        }

        // ───────── Left Nav Handlers (only active once logged in) ─────────

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
            ContentArea.Content = new AssignmentView();
        }

        private void BtnPatientFiles_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new PatientFileView();
        }

        private void BtnInsProviders_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new InsuranceProviderView();
        }

        private void BtnInsPolicies_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new InsurancePolicyView();
        }

        private void BtnAppointments_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new AppointmentView();
        }

        private void BtnPtInsurance_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new PatientInsuranceView();
        }

        private void BtnInvoices_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new InvoiceView();
        }

        private void BtnPayments_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new PaymentView();
        }

        private void BtnMedRecords_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new MedicalRecordView();
        }

        private void BtnPrescriptions_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new PrescriptionView();
        }

        private void BtnDepartments_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new DepartmentView();
        }
    }
}
