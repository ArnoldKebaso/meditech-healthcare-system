// Views/DashboardPlaceholderView.xaml.cs
using System.Windows.Controls;

namespace MediTechDesktopApp.Views
{
    public partial class DashboardPlaceholderView : UserControl
    {
        public DashboardPlaceholderView(string label = "Dashboard Placeholder")
        {
            InitializeComponent();
            TxtPlaceholder.Text = label;
        }
    }
}
