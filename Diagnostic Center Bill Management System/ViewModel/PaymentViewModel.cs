using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diagnostic_Center_Bill_Management_System.Models
{
    public class PaymentViewModel
    {
        public int RequestMasterId { get; set; }
        public int Amount { get; set; }

        [Display(Name = "Due Date")]
        public string Duedate { get; set; }
    }
}