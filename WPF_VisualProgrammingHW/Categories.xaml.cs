using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WPF_VisualProgrammingHW._data;

namespace WPF_VisualProgrammingHW
{
    /// <summary>
    /// Interaction logic for Categories.xaml
    /// </summary>
    public partial class Categories : Page
    {
        public Categories()
        {
            InitializeComponent();
        }

        private DbLibrarySystemEntities _db = new DbLibrarySystemEntities();

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            TBL_CATEGORY newCategory = new TBL_CATEGORY()
            {
                CATEGORY = txtCategory.Text,
                STATUS = true
            };

            _db.TBL_CATEGORY.Add(newCategory);
            await _db.SaveChangesAsync();
            Read();
        }

        private async void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            int categoryId = (dg.SelectedItem as TBL_CATEGORY).ID;
            var categoryToDelete = _db.TBL_CATEGORY.Single(a => a.ID == categoryId);
            _db.TBL_CATEGORY.Remove(categoryToDelete);
            await _db.SaveChangesAsync();
            Read();
        }

        private void DataGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            TxtId.Text = (dg.SelectedItem as TBL_CATEGORY)?.ID.ToString();
            txtCategory.Text = (dg.SelectedItem as TBL_CATEGORY)?.CATEGORY;
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            Read();
        }

        private async void btnUptd_Click(object sender, RoutedEventArgs e)
        {
            int categoryId = (dg.SelectedItem as TBL_CATEGORY).ID;

            TBL_CATEGORY categoryToUpdate = (from a in _db.TBL_CATEGORY where a.ID == categoryId select a).Single();
            categoryToUpdate.CATEGORY = txtCategory.Text;

            await _db.SaveChangesAsync();
            Read();
        }

        private void Categories_OnLoaded(object sender, RoutedEventArgs e)
        {
            Read();
        }

        private void Read()
        {
            dg.ItemsSource = null;
            dg.ItemsSource = _db.TBL_CATEGORY.ToList();
        }
    }
}