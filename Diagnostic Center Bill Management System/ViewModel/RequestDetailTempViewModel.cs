using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diagnostic_Center_Bill_Management_System.ViewModel
{
    public class RequestDetailTempViewModel
    {
        public int SL { get; set; }

        public int TestId { get; set; }
        public string TestName { get; set; }

        public int Fee { get; set; }
    }
}