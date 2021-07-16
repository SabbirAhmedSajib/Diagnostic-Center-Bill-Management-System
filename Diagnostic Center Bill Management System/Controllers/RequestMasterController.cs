using Diagnostic_Center_Bill_Management_System.BAL;
using Diagnostic_Center_Bill_Management_System.Models;
using Diagnostic_Center_Bill_Management_System.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diagnostic_Center_Bill_Management_System.Controllers
{
    public class RequestMasterController : Controller
    {
        private ITestTypeService testTypeService;

        public RequestMasterController()
        {
            this.testTypeService = new TestsTypeService();
        }
        // GET: RequestMaster
        public ActionResult Index()
        {
            AccessModel accessModel = new AccessModel();
            List<TestSetup> getRequestMasterlist = testTypeService.GetAllTestSetup();

            SelectList list = new SelectList(getRequestMasterlist, "Id", "TestName");
            ViewBag.TestSetupId = list;

            if(Session["TempList"] != null)
            {
                accessModel.RequestDetailTempViewModels = Session["TempList"] as List<RequestDetailTempViewModel>;
            }
            if (Session["total"] != null)
            {
                accessModel.Total = Convert.ToInt32( Session["total"].ToString());
            }

            if (TempData["Name"] != null)
            {
                accessModel.PatientName = TempData["Name"].ToString();
            }
            if (TempData["DOB"] != null)
            {
                accessModel.DateOfBirth = TempData["DOB"].ToString();
            }
            if (TempData["Mobile"] != null)
            {
                accessModel.MobileNumber = TempData["Mobile"].ToString();
            }

            return View(accessModel);
        }

        [HttpPost]
        public ActionResult SaveInformation(AccessModel model)
        {
            TestSetup setup = testTypeService.GetTestSetupById(model.TestSetupId);
            int total = Session["total"] == null ? 0 : Convert.ToInt32(Session["total"].ToString());
            int counter = Session["counter"] == null ? 0 : Convert.ToInt32(Session["counter"].ToString());
            counter = counter + 1;

            List<RequestDetailTempViewModel> requestDetailTempViewModels = Session["TempList"] == null ? new List<RequestDetailTempViewModel>() :
                Session["TempList"] as List<RequestDetailTempViewModel>;

            RequestDetailTempViewModel requestDetailTempViewModel = new RequestDetailTempViewModel();

            requestDetailTempViewModel.SL = counter;
            requestDetailTempViewModel.TestId = model.TestSetupId;
            requestDetailTempViewModel.TestName = setup.TestName;
            requestDetailTempViewModel.Fee = setup.Fee;

            requestDetailTempViewModels.Add(requestDetailTempViewModel);
            total = total + setup.Fee;

            Session["TempList"] = requestDetailTempViewModels;
            Session["counter"] = counter;
            Session["total"] = total;


            TempData["Name"] = model.PatientName;
            TempData["DOB"] = model.DateOfBirth;
            TempData["Mobile"] = model.MobileNumber;

            

            return RedirectToAction("Index");
            /*List<TestSetup> getRequestMasterlist = testTypeService.GetAllTestSetup();

            SelectList list = new SelectList(getRequestMasterlist, "Id", "TestName");
            ViewBag.TestSetupId = list;

            requestMaster.UserId = User.Identity.GetUserId();
            requestMaster.EntryDate = DateTime.Now;

            testTypeService.Create(requestMaster);

            return RedirectToAction("Index");*/
        }

        
        [HttpGet]
        public ActionResult LoadAllTestSetup()
        {
            List<TestSetup> data = testTypeService.GetAllTestSetup();

            return PartialView("_TestSetupList", data);
        }

        public JsonResult  Onclick( int TestSetupId)
        {
            TestSetup setup = testTypeService.GetTestSetupById(TestSetupId);

            return Json(setup.Fee, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(AccessModel model)
        {
            model.UserId = User.Identity.GetUserId();
            model.EntryDate = DateTime.Now;
            model.Total = Convert.ToInt32(Session["total"].ToString());

            model.RequestDetailTempViewModels = Session["TempList"] as List<RequestDetailTempViewModel>;

            model = testTypeService.Save(model);

            Session["total"] = null;
            Session["counter"] = null;
            Session["TempList"] = null;

            TempData["billnumber"] = model;
            
            return Json(new { success = true, billnumber =model.BillNumber, message = "Saved Successfully", JsonRequestBehavior.AllowGet });
        }

        
    }
}