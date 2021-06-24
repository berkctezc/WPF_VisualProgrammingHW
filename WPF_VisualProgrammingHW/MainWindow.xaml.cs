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
using WPF_VisualProgrammingHW._data;

namespace WPF_VisualProgrammingHW
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

        private DbLibrarySystemEntities db = new DbLibrarySystemEntities();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var query = (from x in db.TBL_ADMIN
                where x.USERNAME == txtUsername.Text & x.PASSWORD == txtPass.Text
                select x);

            if (query.Any())
            {
                ContentWindow contentWindow = new ContentWindow();
                contentWindow.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password, Please Try Again", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
