using Diagnostic_Center_Bill_Management_System.BAL;
using Diagnostic_Center_Bill_Management_System.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diagnostic_Center_Bill_Management_System.Controllers
{
    public class UnpaidBillReportController : Controller
    {
        private ITestTypeService testsTypeService;

        public UnpaidBillReportController()
        {
            this.testsTypeService = new TestsTypeService();
        }
        // GET: UnpaidBillReport
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoadAllUnpaidResult(String FromDate, String ToDate)
        {
            List<UnpaidResultViewModel> result = testsTypeService.UnpaidShowData(FromDate, ToDate);

            return PartialView("_UnpaidResult", result);
        }
    }
}