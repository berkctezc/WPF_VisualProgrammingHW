using System;
using System.Collections.Generic;

#nullable disable

namespace WPF_HospitalManagementSystem._data
{
    public partial class TblDoctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? Branch { get; set; }
        public int? Title { get; set; }
        public DateTime? Birthofdate { get; set; }
        public string Age { get; set; }
        public bool? Status { get; set; }

        public virtual TblBranch BranchNavigation { get; set; }
    }
}
