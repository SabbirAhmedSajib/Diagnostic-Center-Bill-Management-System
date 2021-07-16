using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diagnostic_Center_Bill_Management_System.ViewModel
{
    public class AccessModel
    {
        public AccessModel()
        {
            RequestDetailTempViewModels = new List<RequestDetailTempViewModel>();
        }
       public string PatientName { get; set; }

      public string MobileNumber { get; set; }
      public string DateOfBirth { get; set; } 

      public int TestSetupId { get; set; }
      public int Fee { get; set; }          

        public int Total { get; set; }

        public int BillNumber { get; set; }
        public string UserId { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }

        public List<RequestDetailTempViewModel> RequestDetailTempViewModels { get; set; }


    }
}