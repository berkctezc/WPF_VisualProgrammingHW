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
using WPF_HospitalManagementSystem._data;
using WPF_HospitalManagementSystem.ViewModels;

namespace WPF_HospitalManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for Doctors.xaml
    /// </summary>
    public partial class Doctors : Page
    {
        public Doctors() => InitializeComponent();

        private static DbHospitalManagementSystemContext _db = new();

        IQueryable<DoctorViewModel> initialData = _db.TblDoctors.Select(x => new DoctorViewModel()
        {
            Id = x.Id,
            Name = x.Name,
            Surname = x.Surname,
            Branch = x.BranchNavigation.Branch,
            Birthofdate = x.Birthofdate,
            Status = x.Status
        });

        private void Doctors_Load(object sender, RoutedEventArgs e) => Read();

        private void Clear()
        {
            txtName.Text = "";
            txtSurname.Text = "";
            txtId.Text = "";
            txtSearch.Text = "";
            comboBranch.Text = "";
            dateBirth.Text = "";
        }

        private void Read()
        {
            dg.ItemsSource = null;

            dg.ItemsSource = initialData.ToList();

            comboBranch.ItemsSource = (from x in _db.TblBranches
                                       select new
                                       {
                                           x.Id,
                                           x.Branch
                                       }).ToList();
        }

        private void DataGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            int? docId = (dg.SelectedItem as DoctorViewModel)?.Id;
            txtId.Text = docId.ToString();
            txtName.Text = (dg.SelectedItem as DoctorViewModel)?.Name;
            txtSurname.Text = (dg.SelectedItem as DoctorViewModel)?.Surname;
            comboBranch.Text = (dg.SelectedItem as DoctorViewModel)?.Branch;
            dateBirth.SelectedDate = (dg.SelectedItem as DoctorViewModel)?.Birthofdate;
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            TblDoctor newDoctor = new TblDoctor()
            {
                Name = txtName.Text,
                Surname = txtName.Text,
                Branch = _db.TblBranches.Single(x => x.Branch == comboBranch.Text).Id,
                Birthofdate = dateBirth.SelectedDate,
                Status = true
            };

            _db.TblDoctors.Add(newDoctor);
            _ = await _db.SaveChangesAsync();
            Clear();
            Read();
        }

        private void btnRead_Click(object sender, RoutedEventArgs e) => Read();

        private async void btnUptd_Click(object sender, RoutedEventArgs e)
        {
            int? docId = (dg.SelectedItem as DoctorViewModel)?.Id;
            if (docId != null)
            {
                TblDoctor docToUpdate = (from a in _db.TblDoctors where a.Id == docId select a).Single();
                docToUpdate.Name = txtName.Text;
                docToUpdate.Surname = txtSurname.Text;
                docToUpdate.Birthofdate = dateBirth.SelectedDate;
                docToUpdate.Branch = _db.TblBranches.Single(x => x.Branch == comboBranch.Text).Id;

                _ = await _db.SaveChangesAsync();
                Clear();
                Read();
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

            dg.ItemsSource = initialData
                .Where(x =>
                    x.Name.Contains(txtSearch.Text) ||
                    x.Surname.Contains(txtSearch.Text) ||
                    x.Branch.Contains(txtSearch.Text)
                ).ToList();
        }

        private async void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            int? docId = (dg.SelectedItem as DoctorViewModel)?.Id;
            if (docId != null)
            {
                TblDoctor docToDel = _db.TblDoctors.Single(a => a.Id == docId);
                _db.TblDoctors.Remove(docToDel);
                _ = await _db.SaveChangesAsync();
                Clear();
                Read();
            }
        }
    }
}
