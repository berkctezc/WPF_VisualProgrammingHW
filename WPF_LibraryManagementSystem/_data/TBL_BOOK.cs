//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPF_LibraryManagementSystem._data
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_BOOK
    {
        public int ID { get; set; }
        public string BOOK { get; set; }
        public Nullable<int> AUTHOR { get; set; }
        public Nullable<int> CATEGORY { get; set; }
        public Nullable<int> PUBLISHER { get; set; }
        public string YEAR { get; set; }
        public string NUMBEROFPAGES { get; set; }
        public Nullable<bool> STATUS { get; set; }
    
        public virtual TBL_AUTHOR TBL_AUTHOR { get; set; }
        public virtual TBL_CATEGORY TBL_CATEGORY { get; set; }
        public virtual TBL_PUBLISHER TBL_PUBLISHER { get; set; }
    }
}
