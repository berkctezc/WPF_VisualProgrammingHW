using System;

#nullable disable

namespace WPF_HospitalManagementSystem._data;

public partial class TblDoctor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int? Branch { get; set; }
    public DateTime? Birthofdate { get; set; }
    public bool? Status { get; set; }

    public virtual TblBranch BranchNavigation { get; set; }
}