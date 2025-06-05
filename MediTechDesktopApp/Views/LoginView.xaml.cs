using MediTechDesktopApp.Services;
using System.Windows;
using System.Windows.Controls;

namespace MediTechDesktopApp.Views
{
    public partial class LoginView : UserControl
    {
        private UserService _userService = new UserService();

        public LoginView()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string role;
            bool isAuthenticated = _userService.Authenticate(txtUsername.Text, txtPassword.Password, out role);

            if (isAuthenticated)
            {
                MessageBox.Show($"Login successful as {role}", "Welcome", MessageBoxButton.OK, MessageBoxImage.Information);
                ((MainWindow)Application.Current.MainWindow).ContentArea.Content = new DashboardView();
            }
            else
            {
                MessageBox.Show("Invalid credentials. Try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
