using System.Collections.Generic;

#nullable disable

namespace WPF_HospitalManagementSystem._data
{
    public sealed class TblBranch
    {
        public TblBranch()
        {
            TblDoctors = new HashSet<TblDoctor>();
            TblNurses = new HashSet<TblNurse>();
        }

        public int Id { get; set; }
        public string Branch { get; set; }
        public bool? Status { get; set; }

        public ICollection<TblDoctor> TblDoctors { get; set; }
        public ICollection<TblNurse> TblNurses { get; set; }
    }
}