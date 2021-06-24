using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
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
    /// Interaction logic for Authors.xaml
    /// </summary>
    public partial class Authors : Page
    {
        public Authors()
        {
            InitializeComponent();
        }

        private DbLibrarySystemEntities _db = new DbLibrarySystemEntities();

        private void Authors_Load(object sender, EventArgs e)
        {
            Read();
        }


        void Read()
        {
            dg.ItemsSource = null;
            dg.ItemsSource = _db.TBL_AUTHOR.ToList();
        }

        private void DataGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            TBL_AUTHOR author = new TBL_AUTHOR
            {
                ID = 1,
                NAME = "a",
                SURNAME = "b",
                STATUS = true
            };
            

            Console.WriteLine(author.ID);

            txtId.Text = author.ID.ToString();
            txtName.Text = author.NAME;
            txtSurname.Text = author.SURNAME;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            TBL_AUTHOR newAuthor = new TBL_AUTHOR()
            {
                NAME = txtName.Text,
                SURNAME = txtSurname.Text,
                STATUS = true
            };

            _db.TBL_AUTHOR.Add(newAuthor);
            _db.SaveChanges();
            Read();
        }
    }
}
