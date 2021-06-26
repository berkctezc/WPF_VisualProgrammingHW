using System;

namespace WPF_LibraryManagementSystem.ViewModels
{
    public class BookViewModel
        {
            public int ID { get; set; }
            public string BOOK { get; set; }
            public string AUTHOR { get; set; }
            public string CATEGORY { get; set; }
            public string PUBLISHER { get; set; }
            public string YEAR { get; set; }
            public string NUMBEROFPAGES { get; set; }
            public bool? STATUS { get; set; }
        }
}