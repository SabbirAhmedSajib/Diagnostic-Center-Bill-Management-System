using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diagnostic_Center_Bill_Management_System.ViewModel
{
    public class TestResultViewModel
    {
        [Key]

        public int Id { get; set; }
        public int SL { get; set; }
        public string TestName { get; set; }

        public int TotalTest { get; set; }

        public int TotalAmount { get; set; }
    }
}