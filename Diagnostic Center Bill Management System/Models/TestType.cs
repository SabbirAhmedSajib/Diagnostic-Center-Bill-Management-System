using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Diagnostic_Center_Bill_Management_System.Models
{
    public class TestType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string TypeName { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }

        public virtual List<TestSetup> TestSetups { get; set; }
    }
}