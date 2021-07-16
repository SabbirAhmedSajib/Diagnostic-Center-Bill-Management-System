using Diagnostic_Center_Bill_Management_System.BAL;
using Diagnostic_Center_Bill_Management_System.Models;
using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diagnostic_Center_Bill_Management_System.Controllers
{
    public class PaymentController : Controller
    {
        private ITestTypeService testTypeService;

        public PaymentController()
        {
            this.testTypeService = new TestsTypeService();
        }

        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Search(string BillNo, string MobileNumber)
        {
            Models.RequestMaster master = testTypeService.ReturnSearch(BillNo, MobileNumber);

            return Json(new { amount = master.Total, duedate = master.PayDueDate.ToShortDateString(), RequestMasterId = master.Id }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Pay(PaymentViewModel payment)
        {

            testTypeService.Pay(payment);

            ViewBag.success = "Successfully updated";

            return View("Index", new PaymentViewModel());
        }
    }
}