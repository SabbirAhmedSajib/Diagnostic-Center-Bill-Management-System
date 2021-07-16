using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Diagnostic_Center_Bill_Management_System.Models
{
    public class RequestMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string PatientName { get; set; }

        public  DateTime DateOfBirth { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }

        public int Total { get; set; }

        public string BillPaymentStatus { get; set; }
        public DateTime PayDueDate { get; set; }

        public virtual List<RequestDetail> RequestDetails { get; set; }

    }
}