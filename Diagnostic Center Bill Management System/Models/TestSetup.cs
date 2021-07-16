using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Diagnostic_Center_Bill_Management_System.Models
{
    public class TestSetup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Test Name")]
        public string TestName { get; set; }

        [Required]
        public int Fee { get; set; }
        [Display(Name = "Test Type")]
        public int TestTypeId { get; set; }

        public virtual TestType TestType { get; set; }

        public string UserId { get; set; }

        public Nullable<System.DateTime> EntryDate { get; set; }

        public virtual List<RequestDetail> RequestDetails{ get; set; }
    }
}