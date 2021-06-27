using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WPF_HospitalManagementSystem._data;

namespace WPF_HospitalManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for Branches.xaml
    /// </summary>
    public partial class Branches : Page
    {
        public Branches()
        {
            InitializeComponent();
        }

        private DbHospitalManagementSystemContext _db = new();

        private void Branches_Load(object sender, EventArgs e)
        {
            Read();
        }

        private void Read()
        {
            dg.ItemsSource = null;
            dg.ItemsSource = _db.TblBranches.ToList();
        }

        private void DataGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            txtId.Text = (dg.SelectedItem as TblBranch)?.Id.ToString();
            txtBranch.Text = (dg.SelectedItem as TblBranch)?.Branch;
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            TblBranch newBranch = new TblBranch()
            {
                Branch = txtBranch.Text,
                Status = true
            };

            if (!txtBranch.Text.Equals(""))
            {
                _db.TblBranches.Add(newBranch);
                _ = await _db.SaveChangesAsync();
                Read();
            }
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            Read();
        }

        private async void btnUptd_Click(object sender, RoutedEventArgs e)
        {
            int? branchId = (dg.SelectedItem as TblBranch)?.Id;
            if (branchId != null)
            {
                TblBranch branchToUpdate = (from a in _db.TblBranches where a.Id == branchId select a).Single();
                branchToUpdate.Branch = txtBranch.Text;

                _ = await _db.SaveChangesAsync();
                Read();
            }
        }

        private async void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            int? branchId = (dg.SelectedItem as TblBranch)?.Id;
            if (branchId != null)
            {
                TblBranch branchToDelete = _db.TblBranches.Single(a => a.Id == branchId);
                _db.TblBranches.Remove(branchToDelete);
                _ = await _db.SaveChangesAsync();
                Read();
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Read();
            dg.ItemsSource = _db.TblBranches.Where(x => x.Branch.Contains(txtSearch.Text)).ToList();
        }
    }
}