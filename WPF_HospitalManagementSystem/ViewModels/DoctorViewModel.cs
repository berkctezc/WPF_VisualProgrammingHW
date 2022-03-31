using System;

namespace WPF_HospitalManagementSystem.ViewModels;

public class DoctorViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Branch { get; set; }
    public DateTime? Birthofdate { get; set; }
    public bool? Status { get; set; }
}