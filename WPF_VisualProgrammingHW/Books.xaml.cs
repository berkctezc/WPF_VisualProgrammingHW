using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_VisualProgrammingHW._data;

namespace WPF_VisualProgrammingHW
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


        void Read()
        {
            dg.ItemsSource = null;
           /* dg.ItemsSource = (from x in _db.TBL_BOOK
                              select new
                              {
                                  x.ID,
                                  x.BOOK,
                                  AUTHOR = x.TBL_AUTHOR.NAME + " " + x.TBL_AUTHOR.SURNAME,
                                  CATEGORY = x.TBL_CATEGORY.CATEGORY,
                                  PUBLISHER = x.TBL_PUBLISHER.PUBLISHER,
                                  x.YEAR,
                                  NUMBEROFPAGES = x.NUMBEROFPAGES,
                                  x.STATUS
                              }).Where(x => x.STATUS == true).ToList();
           */

           dg.ItemsSource = _db.TBL_BOOK
               .Include(x => x.TBL_AUTHOR)
               .Include(x=>x.TBL_CATEGORY)
               .Include(x => x.TBL_PUBLISHER)
               .ToList();
        }

        void Clear()
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
            Console.WriteLine(dg.SelectedItem);


            TxtId.Text = (dg.SelectedItem as TBL_BOOK).ID.ToString();
            //TxtBook.Text = (dg.SelectedItem as TBL_BOOK)?.BOOK;
            //comboAuthor.Text = (dg.SelectedItem as TBL_BOOK)?.TBL_AUTHOR.NAME + " " + (dg.SelectedItem as TBL_BOOK)?.TBL_AUTHOR.SURNAME;
            //comboCategory.Text = (dg.SelectedItem as TBL_BOOK)?.TBL_CATEGORY.CATEGORY;
            //comboPublisher.Text = (dg.SelectedItem as TBL_BOOK)?.TBL_PUBLISHER.PUBLISHER;
            //txtYear.Text = (dg.SelectedItem as TBL_BOOK)?.YEAR;
            //txtPage.Text = (dg.SelectedItem as TBL_BOOK)?.NUMBEROFPAGES;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
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
            _db.SaveChanges();
            Read();
            Clear();
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            Read();
        }

        private void btnUptd_Click(object sender, RoutedEventArgs e)
        {
            //int authorId = (dg.SelectedItem as TBL_AUTHOR).ID;

            //TBL_AUTHOR authorToUpdate = (from a in _db.TBL_AUTHOR where a.ID == authorId select a).Single();
            //authorToUpdate.NAME = txtName.Text;
            //authorToUpdate.SURNAME = txtSurname.Text;

            //_db.SaveChanges();
            //Read();
        }

        private async void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            int bookId = (dg.SelectedItem as TBL_BOOK).ID;
            var bookToDelete = _db.TBL_BOOK.Single(a => a.ID == bookId);
            _db.TBL_BOOK.Remove(bookToDelete);
            await _db.SaveChangesAsync();
            Read();
        }
    }
}
