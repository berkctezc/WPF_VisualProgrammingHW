using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPF_HospitalManagementSystem._data;

namespace WPF_HospitalManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()=>InitializeComponent();

        private DbHospitalManagementSystemContext _db = new DbHospitalManagementSystemContext();
        private ContentWindow contentWindow;

        private void MainWindow_OnClosed(object sender, EventArgs e) => Environment.Exit(0);

        private void txtUsername_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txtUsername.Text == "Username")
                txtUsername.Text = "";
        }

        private void txtPass_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            bool touched = true;
            if (touched)
                txtPass.Password = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var query = (from x in _db.TblAdmins
                         where x.Username == txtUsername.Text & x.Password == txtPass.Password
                         select x);

            if (query.Any())
            {
                MessageBox.Show("Successfully Logged in", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                contentWindow = new ContentWindow();
                contentWindow.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
