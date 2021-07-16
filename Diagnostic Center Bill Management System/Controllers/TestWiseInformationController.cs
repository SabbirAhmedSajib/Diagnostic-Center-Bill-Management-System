using Diagnostic_Center_Bill_Management_System.BAL;
using Diagnostic_Center_Bill_Management_System.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diagnostic_Center_Bill_Management_System.Controllers
{
    public class TestWiseInformationController : Controller
    {
        private ITestTypeService testsTypeService;

        public TestWiseInformationController()
        {
            this.testsTypeService = new TestsTypeService();
        }
        // GET: TestWiseReport
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoadAllTypeResult(String FromDate, String ToDate)
        {
            List<TestWiseReportInformationViewModel> result = testsTypeService.TypeResultData(FromDate, ToDate);

            return PartialView("_TestInformation", result);
        }
    }
}
