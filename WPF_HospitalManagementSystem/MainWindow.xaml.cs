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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_HospitalManagementSystem._data;

namespace WPF_HospitalManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private DbHospitalManagementSystemContext _db = new DbHospitalManagementSystemContext();

        private void MainWindow_OnClosed(object? sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void txtUsername_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txtUsername.Text == "Username")
                txtUsername.Text = "";
        }

        private void txtPass_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txtPass.Password == "password")
                txtPass.Password = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var query = (from x in _db.TblAdmins
                where x.Username == txtUsername.Text & x.Password == txtPass.Password
                select x);

            if (query.Any())
            {
                MessageBox.Show("scs", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                // ContentWindow contentWindow = new ContentWindow();
                //contentWindow.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password, Please Try Again", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
