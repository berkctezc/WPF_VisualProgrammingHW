using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_HospitalManagementSystem.ViewModels
{
    public class NurseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Policlinic { get; set; }
        public DateTime? Birthofdate { get; set; }
        public bool? Status { get; set; }
    }
}
