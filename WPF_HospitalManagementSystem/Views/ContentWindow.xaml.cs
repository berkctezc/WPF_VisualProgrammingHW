using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_HospitalManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for ContentWindow.xaml
    /// </summary>
    public partial class ContentWindow : Window
    {
        public ContentWindow()
        {
            InitializeComponent();
        }

        private Branches _branchesWindow;
        private Doctors _doctorsWindow;
        private Nurses _nursesWindow;

        private void Branches_Click(object sender, RoutedEventArgs e)
        {
            _branchesWindow = new Branches();
            MyFrame.Content = null;
            MyFrame.Content = _branchesWindow;
        }

        private void Doctors_Click(object sender, RoutedEventArgs e)
        {
            _doctorsWindow = new Doctors();
            MyFrame.Content = null;
            MyFrame.Content = _doctorsWindow;
        }

        private void Nurses_Click(object sender, RoutedEventArgs e)
        {
            _nursesWindow = new Nurses();
            MyFrame.Content = null;
            MyFrame.Content = _nursesWindow;
        }

        private void Clear_Click(object sender, RoutedEventArgs e) => MyFrame.Content = null;

        private void ContentWindow_OnClosed(object sender, EventArgs e) => Environment.Exit(0);
    }
}
