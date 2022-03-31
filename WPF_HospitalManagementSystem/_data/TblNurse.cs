using System;

#nullable disable

namespace WPF_HospitalManagementSystem._data;

public partial class TblNurse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int? Policlinic { get; set; }
    public DateTime? Birthofdate { get; set; }
    public bool? Status { get; set; }

    public virtual TblBranch PoliclinicNavigation { get; set; }
}