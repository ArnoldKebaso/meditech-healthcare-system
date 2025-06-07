using System;
using System.Windows;
using System.Windows.Controls;

namespace MediTechDesktopApp.Views
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void GetStarted_Click(object sender, RoutedEventArgs e)
        {
            // Reuse the Dashboard click on MainWindow (now public)
            if (Window.GetWindow(this) is MainWindow mw)
                mw.BtnDashboard_Click(this, null);
        }

        private void LoginNow_Click(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow mw)
                mw.BtnDashboard_Click(this, null);
        }
    }
}
