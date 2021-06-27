using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WPF_HospitalManagementSystem._data;
using WPF_HospitalManagementSystem.ViewModels;

namespace WPF_HospitalManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for Nurses.xaml
    /// </summary>
    public partial class Nurses : Page
    {
        public Nurses() => InitializeComponent();

        private static DbHospitalManagementSystemContext _db = new();

        private IQueryable<NurseViewModel> initialData = _db.TblNurses.Select(x => new NurseViewModel()
        {
            Id = x.Id,
            Name = x.Name,
            Surname = x.Surname,
            Policlinic = x.PoliclinicNavigation.Branch,
            Birthofdate = x.Birthofdate,
            Status = x.Status
        });

        private void Nurses_OnLoaded(object sender, RoutedEventArgs e) => Read();

        private void Clear()
        {
            txtName.Text = "";
            txtSurname.Text = "";
            txtId.Text = "";
            txtSearch.Text = "";
            comboClinic.Text = "";
            dateBirth.Text = "";
        }

        private void Read()
        {
            dg.ItemsSource = null;

            dg.ItemsSource = initialData.ToList();

            comboClinic.ItemsSource = (from x in _db.TblBranches
                                       select new
                                       {
                                           x.Id,
                                           x.Branch
                                       }).ToList();
        }

        private void DataGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            int? nurseId = (dg.SelectedItem as NurseViewModel)?.Id;
            txtId.Text = nurseId.ToString();
            txtName.Text = (dg.SelectedItem as NurseViewModel)?.Name;
            txtSurname.Text = (dg.SelectedItem as NurseViewModel)?.Surname;
            comboClinic.Text = (dg.SelectedItem as NurseViewModel)?.Policlinic;
            dateBirth.SelectedDate = (dg.SelectedItem as NurseViewModel)?.Birthofdate;
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            TblNurse newNurse = new TblNurse()
            {
                Name = txtName.Text,
                Surname = txtSurname.Text,
                Policlinic = _db.TblBranches.SingleOrDefault(x => x.Branch == comboClinic.Text)?.Id,
                Birthofdate = dateBirth.SelectedDate,
                Status = true
            };

            if (!txtName.Text.Equals("") || !txtSurname.Text.Equals(""))
            {
                _db.TblNurses.Add(newNurse);
                _ = await _db.SaveChangesAsync();
                Clear();
                Read();
            }
        }

        private void btnRead_Click(object sender, RoutedEventArgs e) => Read();

        private async void btnUptd_Click(object sender, RoutedEventArgs e)
        {
            int? nurseId = (dg.SelectedItem as NurseViewModel)?.Id;
            if (nurseId != null)
            {
                TblNurse nurseToUpdate = (from a in _db.TblNurses where a.Id == nurseId select a).Single();
                nurseToUpdate.Name = txtName.Text;
                nurseToUpdate.Surname = txtSurname.Text;
                nurseToUpdate.Birthofdate = dateBirth.SelectedDate;
                nurseToUpdate.Policlinic = _db.TblBranches.Single(x => x.Branch == comboClinic.Text).Id;

                _ = await _db.SaveChangesAsync();
                Clear();
                Read();
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Read();
            dg.ItemsSource = initialData
                .Where(x =>
                    x.Name.Contains(txtSearch.Text) ||
                    x.Surname.Contains(txtSearch.Text) ||
                    x.Policlinic.Contains(txtSearch.Text)
                ).ToList();
        }

        private async void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            int? nurseId = (dg.SelectedItem as NurseViewModel)?.Id;
            if (nurseId != null)
            {
                TblNurse nurseToDel = _db.TblNurses.Single(a => a.Id == nurseId);
                _db.TblNurses.Remove(nurseToDel);
                _ = await _db.SaveChangesAsync();
                Clear();
                Read();
            }
        }
    }
}