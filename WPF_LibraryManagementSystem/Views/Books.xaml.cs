using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WPF_LibraryManagementSystem._data;
using WPF_LibraryManagementSystem.ViewModels;

namespace WPF_LibraryManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for Books.xaml
    /// </summary>
    public partial class Books : Page
    {
        public Books() => InitializeComponent();

        private DbLibrarySystemEntities _db = new DbLibrarySystemEntities();

        private void Books_Load(object sender, EventArgs e) => Read();

        private void Read()
        {
            dg.ItemsSource = null;

            dg.ItemsSource = _db.TBL_BOOK.Select(x => new BookViewModel()
            {
                ID = x.ID,
                BOOK = x.BOOK,
                AUTHOR = x.TBL_AUTHOR.NAME + " " + x.TBL_AUTHOR.SURNAME,
                CATEGORY = x.TBL_CATEGORY.CATEGORY,
                PUBLISHER = x.TBL_PUBLISHER.PUBLISHER,
                YEAR = x.YEAR,
                NUMBEROFPAGES = x.NUMBEROFPAGES,
                STATUS = x.STATUS
            }).ToList();

            comboAuthor.ItemsSource = (from x in _db.TBL_AUTHOR
                select new
                {
                    x.ID,
                    AUTHOR = x.NAME + " " + x.SURNAME,
                }).ToList();

            comboCategory.ItemsSource = (from x in _db.TBL_CATEGORY
                select new
                {
                    x.ID,
                    x.CATEGORY
                }).ToList();

            comboPublisher.ItemsSource = (from x in _db.TBL_PUBLISHER
                select new
                {
                    x.ID,
                    x.PUBLISHER
                }).ToList();
        }

        private void Clear()
        {
            TxtId.Text = "";
            TxtBook.Text = "";
            comboAuthor.Text = "";
            comboCategory.Text = "";
            comboPublisher.Text = "";
            txtYear.Text = "";
            txtPage.Text = "";
            TxtBook.Focus();
        }

        private void DataGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            int? bookId = (dg.SelectedItem as BookViewModel)?.ID;
            TxtId.Text = bookId.ToString();
            TxtBook.Text = (dg.SelectedItem as BookViewModel)?.BOOK;
            comboAuthor.Text = (dg.SelectedItem as BookViewModel)?.AUTHOR;
            comboCategory.Text = (dg.SelectedItem as BookViewModel)?.CATEGORY;
            comboPublisher.Text = (dg.SelectedItem as BookViewModel)?.PUBLISHER;
            txtYear.Text = (dg.SelectedItem as BookViewModel)?.YEAR;
            txtPage.Text = (dg.SelectedItem as BookViewModel)?.NUMBEROFPAGES;
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            TBL_BOOK t = new TBL_BOOK
            {
                BOOK = TxtBook.Text,
                AUTHOR = int.Parse(comboAuthor.SelectedValue.ToString()),
                CATEGORY = int.Parse(comboCategory.SelectedValue.ToString()),
                PUBLISHER = int.Parse(comboPublisher.SelectedValue.ToString()),
                YEAR = txtYear.Text,
                NUMBEROFPAGES = txtPage.Text,
                STATUS = true
            };

            _db.TBL_BOOK.Add(t);
            _ = await _db.SaveChangesAsync();
            Clear();
            Read();
        }

        private void btnRead_Click(object sender, RoutedEventArgs e) => Read();

        private async void btnUptd_Click(object sender, RoutedEventArgs e)
        {
            int? bookId = (dg.SelectedItem as BookViewModel)?.ID;
            if (bookId != null)
            {
                TBL_BOOK bookToUpdate = (from a in _db.TBL_BOOK where a.ID == bookId select a).Single();
                bookToUpdate.BOOK = TxtBook.Text;
                bookToUpdate.AUTHOR = int.Parse(comboAuthor.SelectedValue.ToString());
                bookToUpdate.CATEGORY = int.Parse(comboCategory.SelectedValue.ToString());
                bookToUpdate.PUBLISHER = int.Parse(comboPublisher.SelectedValue.ToString());
                bookToUpdate.YEAR = txtYear.Text;
                bookToUpdate.NUMBEROFPAGES = txtPage.Text;

                _ = await _db.SaveChangesAsync();
                Clear();
                Read();
            }
        }

        private async void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            int? bookId = (dg.SelectedItem as BookViewModel)?.ID;
            if (bookId != null)
            {
                TBL_BOOK bookToDelete = _db.TBL_BOOK.Single(a => a.ID == bookId);
                _db.TBL_BOOK.Remove(bookToDelete);
                _ = await _db.SaveChangesAsync();
                Clear();
                Read();
            }
        }
    }
}