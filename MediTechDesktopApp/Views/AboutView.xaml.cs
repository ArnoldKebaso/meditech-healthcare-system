using System.Windows.Controls;

namespace MediTechDesktopApp.Views
{
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class AboutView : UserControl
    {
        public AboutView()
        {
            InitializeComponent();
        }

        // Footer CTA “Log In Now” just forwards to MainWindow’s Dashboard handler:
        private void LoginNow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // find the parent Window and invoke its Dashboard click
            if (System.Windows.Window.GetWindow(this) is MainWindow main)
                main.BtnDashboard_Click(null, null);
        }
    }
}
