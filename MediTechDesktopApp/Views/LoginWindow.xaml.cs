// File: Views/LoginWindow.xaml.cs
using System.Windows;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.Views
{
    public partial class LoginWindow : Window
    {
        private readonly UserService _userService;

        public LoginWindow()
        {
            InitializeComponent();

            // Instantiate your UserService (make sure this class exists in Services/ folder)
            _userService = new UserService();

            // Initially hide the error message
            TxtErrorMessage.Visibility = Visibility.Collapsed;
        }

        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the entered username/password
            string username = TxtUsername.Text.Trim();
            string password = TxtPassword.Password.Trim(); // PasswordBox

            // Basic validation: ensure the fields aren’t empty
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                TxtErrorMessage.Text = "Please enter both username and password.";
                TxtErrorMessage.Visibility = Visibility.Visible;
                return;
            }

            bool valid = false;
            try
            {
                // Call your UserService to check credentials
                valid = _userService.ValidateUser(username, password);
            }
            catch (System.Exception ex)
            {
                // If an exception happens (e.g. DB connection), show it
                TxtErrorMessage.Text = "Login error: " + ex.Message;
                TxtErrorMessage.Visibility = Visibility.Visible;
                return;
            }

            if (valid)
            {
                // Successful login → return DialogResult = true
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                // Invalid credentials → show the error line
                TxtErrorMessage.Text = "Invalid username or password";
                TxtErrorMessage.Visibility = Visibility.Visible;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            // User close/cancel → return DialogResult = false
            this.DialogResult = false;
            this.Close();
        }
    }
}
