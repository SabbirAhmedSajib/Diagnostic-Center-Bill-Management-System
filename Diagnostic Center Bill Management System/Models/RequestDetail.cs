using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Diagnostic_Center_Bill_Management_System.Models
{
    public class RequestDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int RequestMasterId { get; set; }
        public virtual RequestMaster RequestMaster { get; set; }
        public int TestSetupId { get; set; }
        public virtual TestSetup TestSetup { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }

    }
}