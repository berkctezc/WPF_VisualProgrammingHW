using System.Collections.Generic;

#nullable disable

namespace WPF_HospitalManagementSystem._data;

public partial class TblBranch
{
    public TblBranch()
    {
        TblDoctors = new HashSet<TblDoctor>();
        TblNurses = new HashSet<TblNurse>();
    }

    public int Id { get; set; }
    public string Branch { get; set; }
    public bool? Status { get; set; }

    public virtual ICollection<TblDoctor> TblDoctors { get; set; }
    public virtual ICollection<TblNurse> TblNurses { get; set; }
}