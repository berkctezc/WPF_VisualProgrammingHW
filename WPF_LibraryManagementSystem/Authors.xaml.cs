using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WPF_LibraryManagementSystem._data;

namespace WPF_LibraryManagementSystem
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

        private void Read()
        {
            dg.ItemsSource = null;
            dg.ItemsSource = _db.TBL_AUTHOR.ToList();
        }

        private void DataGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            txtId.Text = (dg.SelectedItem as TBL_AUTHOR)?.ID.ToString();
            txtName.Text = (dg.SelectedItem as TBL_AUTHOR)?.NAME;
            txtSurname.Text = (dg.SelectedItem as TBL_AUTHOR)?.SURNAME;
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            TBL_AUTHOR newAuthor = new TBL_AUTHOR()
            {
                NAME = txtName.Text,
                SURNAME = txtSurname.Text,
                STATUS = true
            };

            _db.TBL_AUTHOR.Add(newAuthor);
            await _db.SaveChangesAsync();
            Read();
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            Read();
        }

        private async void btnUptd_Click(object sender, RoutedEventArgs e)
        {
            int authorId = (dg.SelectedItem as TBL_AUTHOR).ID;

            TBL_AUTHOR authorToUpdate = (from a in _db.TBL_AUTHOR where a.ID == authorId select a).Single();
            authorToUpdate.NAME = txtName.Text;
            authorToUpdate.SURNAME = txtSurname.Text;

            await _db.SaveChangesAsync();
            Read();
        }

        private async void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            int authorId = (dg.SelectedItem as TBL_AUTHOR).ID;
            var authorToDelete = _db.TBL_AUTHOR.Single(a => a.ID == authorId);
            _db.TBL_AUTHOR.Remove(authorToDelete);
            await _db.SaveChangesAsync();
            Read();
        }
    }
}