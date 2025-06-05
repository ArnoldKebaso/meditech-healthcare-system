using System.Windows;
using System.Windows.Controls;

namespace MediTechDesktopApp.Views
{
    public partial class LandingPage : UserControl
    {
        public LandingPage()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).ContentArea.Content = new LoginView();
        }
    }
}
