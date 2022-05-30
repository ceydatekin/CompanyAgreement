using CompanyAgreement.Helper;
using CompanyAgreement.Manager;
using CompanyAgreement.Models;
using CompanyAgreement.modelview;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyAgreement.Controllers
{
    public class AcademicianController : Controller
    {
        #region Manager
        CompanyManager companyManager = new CompanyManager();
        CantractSituationManager cantractSituationManager = new CantractSituationManager();
        ContractInformationManager contractInformationManager = new ContractInformationManager();
        CompanyInformationManager companyInformationManager = new CompanyInformationManager();
        CompanyDepartmantManager companyDepartmantManager = new CompanyDepartmantManager();
        CompanyAuthorityManager companyAuthorityManager = new CompanyAuthorityManager();
        DepartmantManager departmantManager = new DepartmantManager();

        private readonly ILogger<AcademicianController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        AcademicianLoginManager academicianLoginManager = new AcademicianLoginManager();
        SessionHelper sessionHelper;

        #endregion

        #region Controller 
        public AcademicianController(ILogger<AcademicianController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            sessionHelper = new SessionHelper(_httpContextAccessor);

        }

        #endregion

        #region log -İn -Out
        public IActionResult AcademicianLogin()
        {
            return View();
        }
        [Route("API/AcademicianLogin")]
        [HttpPost]
        public bool AdminLogin([FromForm] AdminUserModel model)
        {

            var user = academicianLoginManager.GetUser(model.userName, model.password);
            if (user == null)
            {
                return false;
            }
            else
            {
                sessionHelper.Set("UserAcademicianName", model.userName);
                sessionHelper.Set("AcademicianDepartment", user.AcademicianDepartment);
                sessionHelper.Set("UserAcademicianId", user.Id);
                return true;

            }
        }
        public class AdminUserModel
        {
            public string userName { get; set; }
            public string password { get; set; }
        }

        [Route("API/AcademicianLogout")]
        [HttpPost]
        public bool logout()
        {
            sessionHelper.Set("UserAcademicianName", "");
            sessionHelper.Set("UserAcademicianId", 0);
            sessionHelper.Set("AcademicianDepartment", 0);
            return true;
            //  return View("OidbLogin");

        }


        #endregion

        #region Firma Ekleme
        [HttpGet]
        public IActionResult Index()
        {
            if (String.IsNullOrEmpty(sessionHelper.Get("UserAcademicianName")))
            {
                return RedirectToAction("AcademicianLogin");
            }
            return View();
        }

        //Firma Giriş Sayfasındaki Form veri tabanına ekleme API'si
        [HttpPost]
        [Route("API/AddCompanyAcademician")]
        public string addCompany([FromForm] addCompanyModel model)
        {
            {
                companyInformationManager.AddCompanyInformation(model.CompanyInformation_mail, model.CompanyInformation_GSM, model.CompanyInformation_Name, model.CompanyInformation_Surname);
                int CompanyInformationId = companyInformationManager.GetById1(model.CompanyInformation_Name, model.CompanyInformation_Surname, model.CompanyInformation_mail).Id;
                companyManager.AddCompany(model.CompanyName, model.MeetingDate, model.PublicPrivate, CompanyInformationId);
                contractInformationManager.addContractInformation(model.ContractInformation_Mail, model.ContractInformation_Gsm, model.ContractInformation_Adress, model.ContractInformation_Province, model.ContractInformation_District);
                int CompanyId = companyManager.GetById1(model.CompanyName, model.MeetingDate).Id;
                cantractSituationManager.AddContractSituation(model.Situations, model.Description, CompanyId);
                return JsonConvert.SerializeObject(new { success = true, message = "Tebrikler" });
            }
        }
        //Sayfadan gelecek olan firma bilgileri formdata
        public class addCompanyModel
        {
            public string CompanyName { get; set; }//yapıldı
            public DateTime MeetingDate { get; set; }//yapıldı
            public bool PublicPrivate { get; set; }//yapıldı
            public string Situations { get; set; }//yapıldı
            public string Description { get; set; }//website olarak alındı
            public string CompanyInformation_mail { get; set; }//yapıldı
            public string CompanyInformation_GSM { get; set; }
            public string CompanyInformation_Name { get; set; }
            public string CompanyInformation_Surname { get; set; }

            public string ContractInformation_Adress { get; set; }
            public string ContractInformation_Province { get; set; }
            public string ContractInformation_District { get; set; }
            public string ContractInformation_Mail { get; set; }
            public string ContractInformation_Gsm { get; set; }

        }

        #endregion

        #region Firma Listele

        public IActionResult ListCompany()
        {
            if (String.IsNullOrEmpty(sessionHelper.Get("UserAcademicianName")))
            {
                return RedirectToAction("AcademicianLogin");
            }
            var companyDepartmentList = companyDepartmantManager.GetAcademicianCompany( (int)sessionHelper.Getid("UserAcademicianId")).ToList();

            List<Company> companyList = new List<Company>();
            List<CompanyInformation> companyInformation = new List<CompanyInformation>();
            List<CompanyAuthority> companyAuthority = new List<CompanyAuthority>();
            List<ContractSituation> contractSituation = new List<ContractSituation>();
            List<ContractInformation> contractInformation = new List<ContractInformation>();

            foreach (var item in companyDepartmentList)
            {
                companyList.Add(companyManager.GetId(item.CompanyId));
                companyInformation.Add(companyInformationManager.GetCompyIdInformation(item.CompanyId));
                companyAuthority.Add(companyAuthorityManager.GetId(item.CompanyId));
                contractSituation.Add(cantractSituationManager.GetId(item.CompanyId));
                contractInformation.Add(contractInformationManager.GetId(item.CompanyId));

            }

            var companyListViewModel = new AcademicianCompanyListViewModel();
            companyListViewModel.Company = companyList;
            companyListViewModel.CompanyDepartment = companyDepartmentList;
            companyListViewModel.CompanyInformation = companyInformation;
            companyListViewModel.CompanyAuthority = companyAuthority;
            companyListViewModel.ContractSituation = contractSituation;
            companyListViewModel.ContractInformation = contractInformation;
            return View(companyListViewModel);
        }
        #endregion

        #region Firma Kontenjan Ekleme
        //Firma Kontenjan Ekleme

        [HttpPost]
        [Route("API/AddQuotaAcademician")]
        public string addQuota([FromForm] addQuotaModel model)
        {
            try
            {
                companyDepartmantManager.Insert(new Models.CompanyDepartment()
                {
                    CompanyId = model.CompanyId,
                    DepartmentId = model.DepartmentId,
                    Amount = model.Amount,
                });
            }
            catch (Exception)
            {
                companyDepartmantManager.GetObject(model.CompanyId, model.DepartmentId, model.Amount);
            }

            return JsonConvert.SerializeObject(new { success = true, message = "Tebrikler" });

        }
        public IActionResult AddCompanyQuota()
        {
            if (String.IsNullOrEmpty(sessionHelper.Get("UserAcademicianName")))
            {
                return RedirectToAction("AcademicianLogin");
            }
            var addQuotaViewModel = new AddQuotaViewModel();
            var companyDepartmentList = companyDepartmantManager.GetAcademicianCompany((int)sessionHelper.Getid("UserAcademicianId")).ToList();
            List<Company> companyList = new List<Company>();
            companyDepartmentList.ForEach(item => companyList.Add(companyManager.GetId(item.CompanyId)));

            addQuotaViewModel.CompanyDepartment = companyDepartmantManager.AllCompaniesDepartment().ToList();
            addQuotaViewModel.Companies = companyList;
            addQuotaViewModel.Departments = departmantManager.AllDepartments().ToList();

            return View(addQuotaViewModel);
        }

        public class addQuotaModel
        {
            public int CompanyId { get; set; }
            public int DepartmentId { get; set; }
            public int Amount { get; set; }


        }


        [HttpGet]
        [Route("API/quotaListAcademician")]
        public string QuotaList(int companyId)
        {
            var companies = companyDepartmantManager.GetAllDepartment(companyId);
            var list = (from _company in companies
                        select new
                        {
                            DepartmentName = _company.Department.DepartmentName,
                            Kontenjan = _company.Amount

                        }).ToList();
            return JsonConvert.SerializeObject(new { success = true, message = "Tebirkler", data = list });
        }


        [Route("API/selectDepertmentAcademician")]
        public int SelectDepertment(int companyId, int departmentId)
        {
            var amount = companyDepartmantManager.GetId(companyId, departmentId);

            return amount;
        }

        #endregion

    }
}
