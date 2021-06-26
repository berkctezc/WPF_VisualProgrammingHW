using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WPF_LibraryManagementSystem._data;

namespace WPF_LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for Books.xaml
    /// </summary>
    public partial class Books : Page
    {
        public Books()
        {
            InitializeComponent();
        }

        private DbLibrarySystemEntities _db = new DbLibrarySystemEntities();

        private void Books_Load(object sender, EventArgs e)
        {
            Read();
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
            }).ToList<BookViewModel>();
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

        public class BookViewModel
        {
            public int ID { get; set; }
            public string BOOK { get; set; }
            public string AUTHOR { get; set; }
            public string CATEGORY { get; set; }
            public string PUBLISHER { get; set; }
            public string YEAR { get; set; }
            public string NUMBEROFPAGES { get; set; }
            public Nullable<bool> STATUS { get; set; }
        }

        private void DataGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            int? BookId = (dg.SelectedItem as BookViewModel)?.ID;
            TxtId.Text = BookId.ToString();
            TxtBook.Text = (dg.SelectedItem as BookViewModel)?.BOOK; //TxtBook.Text = _db.TBL_BOOK.Where(x => x.ID==BookId).Select(x=>x.BOOK).Single().ToString();
            comboAuthor.Text = (dg.SelectedItem as BookViewModel)?.AUTHOR; //_db.TBL_BOOK.Where(x => x.ID==BookId).Select(x=>x.TBL_AUTHOR.NAME).Single().ToString();
            comboCategory.Text = (dg.SelectedItem as BookViewModel)?.CATEGORY;
            comboPublisher.Text = (dg.SelectedItem as BookViewModel)?.PUBLISHER;
            txtYear.Text = (dg.SelectedItem as BookViewModel)?.YEAR;
            txtPage.Text = (dg.SelectedItem as BookViewModel)?.NUMBEROFPAGES;
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            TBL_BOOK t = new TBL_BOOK();
            t.BOOK = TxtBook.Text;
            t.AUTHOR = int.Parse(comboAuthor.SelectedValue.ToString());
            t.CATEGORY = int.Parse(comboCategory.SelectedValue.ToString());
            t.PUBLISHER = int.Parse(comboPublisher.SelectedValue.ToString());
            t.YEAR = txtYear.Text;
            t.NUMBEROFPAGES = txtPage.Text;
            t.STATUS = true;

            _db.TBL_BOOK.Add(t);
            await _db.SaveChangesAsync();
            Read();
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            Read();
        }

        private async void btnUptd_Click(object sender, RoutedEventArgs e)
        {
            int bookId = (dg.SelectedItem as BookViewModel).ID;

            TBL_BOOK bookToUpdate = (from a in _db.TBL_BOOK where a.ID == bookId select a).Single();
            bookToUpdate.BOOK = TxtBook.Text;
            bookToUpdate.AUTHOR = int.Parse(comboAuthor.SelectedValue.ToString());
            bookToUpdate.CATEGORY = int.Parse(comboCategory.SelectedValue.ToString());
            bookToUpdate.PUBLISHER = int.Parse(comboPublisher.SelectedValue.ToString());
            bookToUpdate.YEAR = txtYear.Text;
            bookToUpdate.NUMBEROFPAGES = txtPage.Text;

            await _db.SaveChangesAsync();
            Read();
        }

        private async void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            int bookId = (dg.SelectedItem as BookViewModel).ID;
            var bookToDelete = _db.TBL_BOOK.Single(a => a.ID == bookId);
            _db.TBL_BOOK.Remove(bookToDelete);
            await _db.SaveChangesAsync();
            Read();
        }
    }
}