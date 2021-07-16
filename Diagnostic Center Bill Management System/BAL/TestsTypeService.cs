using Diagnostic_Center_Bill_Management_System.DataRepository;
using Diagnostic_Center_Bill_Management_System.Models;
using Diagnostic_Center_Bill_Management_System.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diagnostic_Center_Bill_Management_System.BAL
{
    public class TestsTypeService : ITestTypeService
    {
        private IRepositoryTestsType repositoryTestsType;

        public TestsTypeService()
        {
            this.repositoryTestsType = new Repository();
        }

        public List<TestType> GetAllTestType()
        {
            return repositoryTestsType.GetAllTestType();
        }

        public void Create(TestType TestsType)
        {
            repositoryTestsType.Create(TestsType);
        }

        public bool CheckName(string name)
        {
            return repositoryTestsType.CheckName(name);
        }


        //Test Setup Service 

        public List<TestSetup> GetAllTestSetup()
        {
            return repositoryTestsType.GetAllTestSetup();
        }

        public void Create(TestSetup testSetup)
        {
            repositoryTestsType.Create(testSetup);
        }

        public bool CheckTestName(string name)
        {
            return repositoryTestsType.CheckTestName(name);
        }

        public TestSetup GetTestSetupById(int id)
        {
            return repositoryTestsType.GetTestSetupById(id);
        }


        //Request Master Information

        public List<RequestMaster> GetAllRequestMaster()
        {
            return repositoryTestsType.GetAllRequestMaster();
        }

        public void Create(RequestMaster requestMaster)
        {
            repositoryTestsType.Create(requestMaster);
        }

        public AccessModel Save(AccessModel model)
        {
            return repositoryTestsType.Save(model);
        }

        public RequestMaster ReturnSearch(string billno, string mobilenumber)
        {
            return repositoryTestsType.ReturnSearch(billno, mobilenumber);
        }

        public void Pay(PaymentViewModel payment)
        {
            repositoryTestsType.Pay(payment);
        }

       public List<TestResultViewModel> ReturnData(string From, string To)
        {
            return repositoryTestsType.ReturnData(From, To);
        }

        public List<UnpaidResultViewModel> UnpaidShowData(string From, string To)
        {
            return repositoryTestsType.UnpaidShowData(From, To);
        }

        public List<TestWiseReportInformationViewModel> TypeResultData(string From, string To)
        {
            return repositoryTestsType.TypeResultData(From, To);
        }
    }
}