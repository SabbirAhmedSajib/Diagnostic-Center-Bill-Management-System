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
    public class TestTypeController : Controller
    {
        private ITestTypeService testsTypeService;

        public TestTypeController()
        {
            this.testsTypeService = new TestsTypeService();
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: TestType
        [HttpPost]
        public ActionResult SaveInformation(TestType testType)
        {
            if (ModelState.IsValid)
            {
                //unique check
                if (!CheckUniqueName(testType.TypeName))
                {
                    testType.UserId = User.Identity.GetUserId();
                    testType.EntryDate = DateTime.Now;

                    testsTypeService.Create(testType);

                    RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("TypeName", "Name already exist");

                    return View("Index", testType);
                }                
            }            

            return View("Index", testType);
        }

        private bool CheckUniqueName(string name)
        {
            return testsTypeService.CheckName(name);
        }

        [HttpGet]
        public ActionResult LoadAllTestType()
        {
            List<TestType> result = testsTypeService.GetAllTestType();

            return PartialView("_TestTypeList", result);
        }
    }
}