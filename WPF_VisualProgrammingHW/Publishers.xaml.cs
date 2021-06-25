using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WPF_VisualProgrammingHW._data;

namespace WPF_VisualProgrammingHW
{
    /// <summary>
    /// Interaction logic for Publishers.xaml
    /// </summary>
    public partial class Publishers : Page
    {
        public Publishers()
        {
            InitializeComponent();
        }

        private DbLibrarySystemEntities _db = new DbLibrarySystemEntities();

        private void Publishers_OnLoaded_OnLoaded(object sender, RoutedEventArgs e) => Read();

        private void Read()
        {
            dg.ItemsSource = null;
            dg.ItemsSource = _db.TBL_PUBLISHER.ToList();
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            TBL_PUBLISHER newPublisher = new TBL_PUBLISHER()
            {
                PUBLISHER = txtPublisher.Text,
                STATUS = true
            };

            _db.TBL_PUBLISHER.Add(newPublisher);
            await _db.SaveChangesAsync();
            Read();
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            Read();
        }

        private async void btnUptd_Click(object sender, RoutedEventArgs e)
        {
            int publisherId = (dg.SelectedItem as TBL_PUBLISHER).ID;

            TBL_PUBLISHER publisherToUpdate = (from a in _db.TBL_PUBLISHER where a.ID == publisherId select a).Single();
            publisherToUpdate.PUBLISHER = txtPublisher.Text;

            await _db.SaveChangesAsync();
            Read();
        }

        private async void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            int publisherId = (dg.SelectedItem as TBL_PUBLISHER).ID;
            var publisherToDel = _db.TBL_PUBLISHER.Single(a => a.ID == publisherId);
            _db.TBL_PUBLISHER.Remove(publisherToDel);
            await _db.SaveChangesAsync();
            Read();
        }

        private void DataGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            TxtId.Text = (dg.SelectedItem as TBL_PUBLISHER)?.ID.ToString();
            txtPublisher.Text = (dg.SelectedItem as TBL_PUBLISHER)?.PUBLISHER;
        }
    }
}