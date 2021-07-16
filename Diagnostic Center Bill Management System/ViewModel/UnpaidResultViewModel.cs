using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diagnostic_Center_Bill_Management_System.ViewModel
{
    public class UnpaidResultViewModel
    {
        [Key]
        public int Id { get; set; }

        public int SL { get; set; }
        public int Billno { get; set; }

        public string Contactno { get; set; }
        public string PatientName { get; set; }
        public int BillAmount { get; set; }

    }
}