using Diagnostic_Center_Bill_Management_System.Models;
using Diagnostic_Center_Bill_Management_System.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diagnostic_Center_Bill_Management_System.BAL
{
    public interface ITestTypeService
    {
        List<TestType> GetAllTestType();
        void Create(TestType TestType);

        bool CheckName(string name);

        // Test Setup part

        List<TestSetup> GetAllTestSetup();
        void Create(TestSetup testSetup);

        bool CheckTestName(string name);

        TestSetup GetTestSetupById(int id);


        //Request Master Information

        List<RequestMaster> GetAllRequestMaster();

        void Create(RequestMaster requestMaster);

        AccessModel Save(AccessModel model);
        RequestMaster ReturnSearch(string billno, string mobilenumber);

        void Pay(PaymentViewModel payment);

        List<TestResultViewModel> ReturnData(string From, string To);

        List<UnpaidResultViewModel> UnpaidShowData(string From, string To);

        List<TestWiseReportInformationViewModel> TypeResultData(string From, string To);
    }
}