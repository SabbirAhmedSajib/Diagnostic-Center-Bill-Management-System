using Diagnostic_Center_Bill_Management_System.BAL;
using Diagnostic_Center_Bill_Management_System.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Diagnostic_Center_Bill_Management_System.Controllers
{
    public class TestSetupController : Controller
    {
        private ITestTypeService testTypeService;        

        public TestSetupController()
        {
            this.testTypeService = new TestsTypeService();
        }
        // GET: TestSetup
        public ActionResult Index()
        {
            List<TestType> gettestsetuplist = testTypeService.GetAllTestType();

            SelectList list = new SelectList(gettestsetuplist, "Id", "TypeName");
            ViewBag.TestTypeId = list;

            return View();
        }

        // GET: TestType
        [HttpPost]
        public ActionResult SaveInformation(TestSetup testSetup)
        {
            List<TestType> gettestsetuplist = testTypeService.GetAllTestType();

            SelectList list = new SelectList(gettestsetuplist, "Id", "TypeName");
            ViewBag.TestTypeId = list;

            if (ModelState.IsValid)
            {
                //unique check
                if (!CheckTestName(testSetup.TestName))
                {
                    testSetup.UserId = User.Identity.GetUserId();
                    testSetup.EntryDate = DateTime.Now;

                    testTypeService.Create(testSetup);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("TestName", "Name already exist");

                    return View("Index", testSetup);
                }
            }

            return View("Index", testSetup);
        }

        private bool CheckTestName(string name)
        {
            return testTypeService.CheckTestName(name);
        }

        [HttpGet]
        public ActionResult LoadAllTestSetup()
        {
            List<TestSetup> data = testTypeService.GetAllTestSetup();

            return PartialView("_TestSetupList", data);
        }
    }
}