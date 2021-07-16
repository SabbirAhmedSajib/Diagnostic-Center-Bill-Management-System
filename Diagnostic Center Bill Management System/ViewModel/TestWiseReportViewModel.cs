using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diagnostic_Center_Bill_Management_System.ViewModel
{
    public class TestWiseReportViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "From Date")]
        public string FromDate { get; set; }

        [Display(Name = "To Date")]
        public string ToDate { get; set; }
    }
}