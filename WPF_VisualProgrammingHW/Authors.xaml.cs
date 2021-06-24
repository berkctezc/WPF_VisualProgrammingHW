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

        private DbLibrarySystemEntities db = new DbLibrarySystemEntities();

        private void Authors_Load(object sender, EventArgs e)
        {
            Read();
        }


        void Read()
        {
            DataGrid.ItemsSource = (from x in db.TBL_AUTHOR
                select new
                {
                    x.ID,
                    x.NAME,
                    x.SURNAME,
                    x.STATUS
                }).Where(x => x.STATUS == true).ToList();
        }
    }
}
