#nullable disable

namespace WPF_HospitalManagementSystem._data;

public partial class TblAdmin
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool? Status { get; set; }
}