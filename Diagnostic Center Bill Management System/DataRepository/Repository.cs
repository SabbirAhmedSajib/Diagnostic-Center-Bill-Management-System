using Diagnostic_Center_Bill_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Configuration;
using System.Data.SqlClient;
using Diagnostic_Center_Bill_Management_System.ViewModel;

namespace Diagnostic_Center_Bill_Management_System.DataRepository
{
    public class Repository : IRepositoryTestsType
    {

        private ApplicationDbContext context;

        public Repository()
        {

            context = new ApplicationDbContext();
        }
        public List<TestType> GetAllTestType()
        {
            return context.TestTypes.OrderBy(x => x.TypeName).ToList();
        }

        public void Create(TestType testType)
        {
            context.TestTypes.Add(testType);
            context.SaveChanges();
        }

        public bool CheckName(string name)
        {
            return context.TestTypes.Where(x => x.TypeName == name).Any();
        }

        // Test Setup Information
        public List<TestSetup> GetAllTestSetup()
        {
            return context.TestSetup.OrderBy(x => x.TestName).ToList();
        }

        public void Create(TestSetup testSetup)
        {
            context.TestSetup.Add(testSetup);
            context.SaveChanges();
        }

        public bool CheckTestName(string name)
        {
            return context.TestSetup.Where(x => x.TestName == name).Any();
        }

        public TestSetup GetTestSetupById(int id)
        {
            return context.TestSetup.Find(id);
        }

        //Request Master Information

        public List<RequestMaster> GetAllRequestMaster()
        {
            return context.RequestMasters.ToList();
        }

        public void Create(RequestMaster requestMaster)
        {
            context.RequestMasters.Add(requestMaster);
            context.SaveChanges();
        }

        private string convertSystemDate(string inputDate)
        {
            string date = "";
            string[] BirthDate = inputDate.Split('-');
            date = BirthDate[2] + "-" + BirthDate[1] + "-" + BirthDate[0];
            return date;
        }

        public AccessModel Save(AccessModel model)
        {
            string systemDate = convertSystemDate(model.DateOfBirth);

            RequestMaster request = new RequestMaster();

            request.DateOfBirth = DateTime.Parse(model.DateOfBirth);//systemDate
            request.PatientName = model.PatientName;
            request.MobileNumber = model.MobileNumber;
            request.UserId = model.UserId;
            request.EntryDate = model.EntryDate;
            request.Total = model.Total;
            request.BillPaymentStatus = "Unpaid";
            request.PayDueDate = DateTime.Now.AddDays(1);

            List<RequestDetail> detail = new List<RequestDetail>();

            for (int i = 0; i < model.RequestDetailTempViewModels.Count; i++)
            {
                RequestDetail requestDetail = new RequestDetail();

                requestDetail.TestSetupId = model.RequestDetailTempViewModels[i].TestId;
                requestDetail.UserId = model.UserId;
                requestDetail.EntryDate = model.EntryDate;

                detail.Add(requestDetail);
            }

            context.RequestMasters.Add(request);
            context.RequestDetails.AddRange(detail);
            context.SaveChanges();

            model.BillNumber = request.Id;

            return model;
        }

        public RequestMaster ReturnSearch(string billno, string mobilenumber)
        {
            if (billno != "")
            {
                int billid = Convert.ToInt32(billno);
                return context.RequestMasters.Where(x => x.Id == billid).FirstOrDefault();
            }
            else
            {
                return context.RequestMasters.Where(x => x.MobileNumber == mobilenumber).FirstOrDefault();
            }
        }


        public void Pay(PaymentViewModel payment)
        {


            //context.ExecuteSqlCommand("select count(RequestDetails.TestSetupId) as id, TestSetups.TestName, sum(testsetups.fee) as fee from RequestDetails inner join TestSetups on RequestDetails.TestSetupId = TestSetups.Id where RequestDetails.entrydate between '2021/02/27' and '2022/07/14' group by RequestDetails.TestSetupId, TestSetups.TestName, TestSetups.Fee");

            var result = context.RequestMasters.Find(payment.RequestMasterId);

            result.BillPaymentStatus = "Paid";
            context.Entry(result).State = EntityState.Modified;

            context.SaveChanges();

        }

        public List<TestResultViewModel> ReturnData(string From, string To)
        {
            List<TestResultViewModel> result = new List<TestResultViewModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@From";
                param.Value = From;

                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@To";
                param1.Value = To;

                cmd.Parameters.Add(param1);

                cmd.CommandText = "select ROW_NUMBER() Over (Order by TestSetups.TestName) As [SL],count(RequestDetails.TestSetupId) as TotalTest, TestSetups.TestName, sum(testsetups.fee) as TotalAmount from RequestDetails inner join TestSetups on RequestDetails.TestSetupId = TestSetups.Id where RequestDetails.entrydate between '" + From + "' and '" + To + "' group by RequestDetails.TestSetupId, TestSetups.TestName, TestSetups.Fee";
                //Dictionary<int, string> dict = new Dictionary<int, string>();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        TestResultViewModel model = new TestResultViewModel();
                        string a = rdr["SL"].ToString();

                        string Name = rdr["TestName"].ToString();

                        string Test = rdr["TotalTest"].ToString();

                        string Amount = rdr["TotalAmount"].ToString();
                        //var b = rdr.GetString(1);

                        model.SL = Convert.ToInt32(a);
                        model.TestName = Name;
                        model.TotalTest = Convert.ToInt32(Test);
                        model.TotalAmount = Convert.ToInt32(Amount);
                        result.Add(model);
                    }
                }

                return result;
            }
        }


        public List<UnpaidResultViewModel> UnpaidShowData(string From, string To)
        {
            List<UnpaidResultViewModel> result = new List<UnpaidResultViewModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@From";
                param.Value = From;

                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@To";
                param1.Value = To;

                cmd.Parameters.Add(param1);

                cmd.CommandText = "select ROW_NUMBER() Over (Order by RequestMasters.id) As [SL], RequestMasters.Id as BillNo, (RequestMasters.MobileNumber) As [Contact No],(RequestMasters.PatientName) As [Patient Name], sum(RequestMasters.Total) As [Bill Amount] from RequestMasters where RequestMasters.BillPaymentstatus = 'unpaid' and RequestMasters.EntryDate between '2021-01-01' and '2022-07-14' Group By RequestMasters.Id,RequestMasters.PatientName, RequestMasters.MobileNumber,RequestMasters.Total";
                //Dictionary<int, string> dict = new Dictionary<int, string>();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        UnpaidResultViewModel model = new UnpaidResultViewModel();
                        string a = rdr["SL"].ToString();

                        string BullNumber = rdr["BillNo"].ToString();

                        string Contact = rdr["Contact No"].ToString();
                        string Patient = rdr["Patient Name"].ToString();

                        string Amount = rdr["Bill Amount"].ToString();
                        //var b = rdr.GetString(1);

                        model.SL = Convert.ToInt32(a);
                        model.Billno = Convert.ToInt32(BullNumber);
                        model.Contactno = Contact;
                        model.PatientName = Patient;
                        model.BillAmount = Convert.ToInt32(Amount);
                        result.Add(model);
                    }
                }

                return result;
            }
        }


        public List<TestWiseReportInformationViewModel> TypeResultData(string From, string To)
        {
            List<TestWiseReportInformationViewModel> result = new List<TestWiseReportInformationViewModel>();

            using (var con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@From";
                param.Value = From;

                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@To";
                param1.Value = To;

                cmd.Parameters.Add(param1);

                cmd.CommandText = "select ROW_NUMBER() Over (Order by TestTypes.TypeName) As [SL],TestTypes.TypeName As [Test Type Name], count( TestSetups.TestTypeId) As [Total no Of Test], sum(TestSetups.Fee) As [Total Amount] FROM TestTypes INNER JOIN TestSetups ON TestTypes.Id = TestSetups.TestTypeId INNER JOIN RequestDetails ON TestSetups.Id = RequestDetails.TestSetupId where RequestDetails.EntryDate between '2021-01-01' and '2022-07-14' group by TestTypes.TypeName,TestSetups.TestTypeId, TestSetups.Fee";
                //Dictionary<int, string> dict = new Dictionary<int, string>();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        TestWiseReportInformationViewModel model = new TestWiseReportInformationViewModel();
                        string a = rdr["SL"].ToString();

                        string Name = rdr["Test Type Name"].ToString();

                        string testno = rdr["Total no Of Test"].ToString();
                        string Amount = rdr["Total Amount"].ToString();

                        model.SL = Convert.ToInt32(a);
                        model.TypeName = Name;
                        model.TotalTest = Convert.ToInt32(testno);
                        model.TotalAmount = Convert.ToInt32(Amount);
                        result.Add(model);
                    }
                }

                return result;
            }
        }
    }
}
    
