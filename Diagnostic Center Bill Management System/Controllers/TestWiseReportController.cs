using Diagnostic_Center_Bill_Management_System.BAL;
using Diagnostic_Center_Bill_Management_System.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diagnostic_Center_Bill_Management_System.Controllers
{
    public class TestWiseReportController : Controller
    {
        private ITestTypeService testsTypeService;

        public TestWiseReportController()
        {
            this.testsTypeService = new TestsTypeService();
        }
        // GET: TestWiseReport
        public ActionResult Index()
        {
            return View();
        }




        [HttpGet]
        public ActionResult LoadAllTestResult(String FromDate, String ToDate)
        {
            List<TestResultViewModel> result = testsTypeService.ReturnData(FromDate, ToDate);

            return PartialView("_TestResult", result);
        }
       
    }
}