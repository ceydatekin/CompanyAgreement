using CompanyAgreement.Helper;
using CompanyAgreement.Manager;
using CompanyAgreement.Models;
using CompanyAgreement.modelview;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace CompanyAgreement.Controllers
{
    public class CompanyController : Controller
    {
        #region Manager
        CompanyManager companyManager = new CompanyManager();
        CantractSituationManager cantractSituationManager = new CantractSituationManager();
        ContractInformationManager contractInformationManager = new ContractInformationManager();
        CompanyInformationManager companyInformationManager = new CompanyInformationManager();
        CompanyDepartmantManager companyDepartmantManager = new CompanyDepartmantManager();
        CompanyAuthorityManager companyAuthorityManager = new CompanyAuthorityManager();
        DepartmantManager departmantManager = new DepartmantManager();

        private readonly ILogger<CompanyController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        CompanyLoginManager companyLoginManager = new CompanyLoginManager();
        SessionHelper sessionHelper;
        #endregion

        #region Controller 
        public CompanyController(ILogger<CompanyController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            sessionHelper = new SessionHelper(_httpContextAccessor);

        }

        #endregion

        #region Login kontrolü session yapısı
        public IActionResult CompanyLogin()
        {
            return View();
        }

        [Route("API/CompanyLogin")]
        [HttpPost]
        public bool companyLogin([FromForm] AdminUserModel model)
        {

            var user = companyLoginManager.GetUser(model.userName, model.password);
            if (user == null)
            {
                return false;
            }
            else
            {
                sessionHelper.Set("UserCompanyName", model.userName);
                sessionHelper.Set("UserCompanyId", user.Companyid);

                return true;

            }
        }
        public class AdminUserModel
        {
            public string userName { get; set; }
            public string password { get; set; }
        }

        [Route("API/CompanyLogout")]
        [HttpPost]
        public bool logoutCompany()
        {
            sessionHelper.Set("UserCompanyName", "");
            sessionHelper.Set("UserCompanyId", 0);
            return true;
            //  return View("OidbLogin");

        }
        #endregion

        #region Firma Ekleme
        [HttpGet]
        public IActionResult Index()
        {
            if (String.IsNullOrEmpty(sessionHelper.Get("UserCompanyName")))
            {
                return RedirectToAction("CompanyLogin");
            }
            var sessionid = sessionHelper.Getid("UserCompanyId");
            var companyViewmodel = new CompanyViewModel();
            companyViewmodel.Company = companyManager.GetId((int)sessionid);
            companyViewmodel.CompanyDepartment = companyDepartmantManager.AllCompaniesDepartment().ToList();
            companyViewmodel.CompanyInformation = companyInformationManager.GetCompyIdInformation((int)sessionid);
            companyViewmodel.CompanyAuthority = companyAuthorityManager.GetId((int)sessionid);
            companyViewmodel.ContractSituation = cantractSituationManager.GetId((int)sessionid);
            companyViewmodel.ContractInformation = contractInformationManager.GetId((int)sessionid);
            return View(companyViewmodel);
        }

        //Firma Giriş Sayfasındaki Form veri tabanına ekleme API'si
        [HttpPost]
        [Route("API/CompanyPage")]
        public string addCompany([FromForm] addCompanyModel model)
        {
            {
                //companyInformationManager.AddCompanyInformation(model.CompanyInformation_mail, model.CompanyInformation_GSM, model.CompanyInformation_Name, model.CompanyInformation_Surname);
                //int CompanyInformationId = companyInformationManager.GetById1(model.CompanyInformation_Name, model.CompanyInformation_Surname, model.CompanyInformation_mail).Id;
                //companyManager.AddCompany(model.CompanyName, model.MeetingDate, model.PublicPrivate, CompanyInformationId);
                //contractInformationManager.addContractInformation(model.ContractInformation_Mail, model.ContractInformation_Gsm, model.ContractInformation_Adress, model.ContractInformation_Province, model.ContractInformation_District);
                //int CompanyId = companyManager.GetById1(model.CompanyName, model.MeetingDate).Id;
                //cantractSituationManager.AddContractSituation(model.Situations, model.Description, CompanyId);
                companyAuthorityManager.addCompanyAuthority(model.CompanyAuthority_SGKNO, model.CompanyAuthority_TaxNumber, model.CompanyAuthority_ContractDate, (int)sessionHelper.Getid("UserCompanyId"));
                return JsonConvert.SerializeObject(new { success = true, message = "Tebrikler" });
            }
        }
        //Sayfadan gelecek olan firma bilgileri formdata
        public class addCompanyModel
        {
            //public string CompanyName { get; set; }//yapıldı
            //public DateTime MeetingDate { get; set; }//yapıldı
            //public bool PublicPrivate { get; set; }//yapıldı
            //public string Situations { get; set; }//yapıldı
            //public string Description { get; set; }//website olarak alındı
            //public string CompanyInformation_mail { get; set; }//yapıldı
            //public string CompanyInformation_GSM { get; set; }
            //public string CompanyInformation_Name { get; set; }
            //public string CompanyInformation_Surname { get; set; }
            //public string ContractInformation_Adress { get; set; }
            //public string ContractInformation_Province { get; set; }
            //public string ContractInformation_District { get; set; }
            //public string ContractInformation_Mail { get; set; }
            //public string ContractInformation_Gsm { get; set; }
            public string CompanyAuthority_SGKNO { get; set; }
            public string CompanyAuthority_TaxNumber { get; set; }
            public DateTime CompanyAuthority_ContractDate { get; set; }

        }

        #endregion
       
        #region Firma Kontenjan Ekleme
        //Firma Kontenjan Ekleme
        [HttpPost]
        [Route("API/AddQuotaCompany")]
        public string addQuota([FromForm] addQuotaModel model)
        {
            try
            {
                companyDepartmantManager.Insert(new Models.CompanyDepartment()
                {
                    CompanyId = (int)sessionHelper.Getid("UserCompanyId"),
                    DepartmentId = model.DepartmentId,
                    Amount = model.Amount,
                });
            }
            catch (Exception)
            {
                companyDepartmantManager.GetObject(1, model.DepartmentId, model.Amount);
            }

            return JsonConvert.SerializeObject(new { success = true, message = "Tebrikler" });

        }
        public IActionResult AddCompanyQuota()
        {
            if (String.IsNullOrEmpty(sessionHelper.Get("UserCompanyName")))
            {
                return RedirectToAction("CompanyLogin");
            }
            var addQuotaViewModel = new CompanyQuotaViewModel();
            addQuotaViewModel.CompanyDepartment = companyDepartmantManager.AllCompaniesDepartment().ToList();
            addQuotaViewModel.Departments = departmantManager.AllDepartments().ToList();
            return View(addQuotaViewModel);
        }

        public class addQuotaModel
        {
            public int DepartmentId { get; set; }
            public int Amount { get; set; }


        }
        [HttpPost]
        [Route("API/UpdateQuota")]
        public string updateQuota([FromForm] updateQuotaModel model)
        {
            try
            {
                companyDepartmantManager.Insert(new Models.CompanyDepartment()
                {
                    DepartmentId = departmantManager.FindDepartmentNameId(model.ModalDepartmentName),
                    Amount = model.ModalAmount,
                });
            }
            catch (Exception)
            {
                companyDepartmantManager.GetObject((int)sessionHelper.Getid("UserCompanyId"), departmantManager.FindDepartmentNameId(model.ModalDepartmentName), model.ModalAmount);
            }

            return JsonConvert.SerializeObject(new { success = true, message = "Tebrikler" });

        }

        public class updateQuotaModel
        {
            public string ModalDepartmentName { get; set; }
            public int ModalAmount { get; set; }


        }
        [HttpGet]
        [Route("API/quotaListCompany")]
        public string QuotaList()
        {
            var companies = companyDepartmantManager.GetAllDepartment((int)sessionHelper.Getid("UserCompanyId"));
            var list = (from _company in companies
                        select new
                        {
                            DepartmentName = _company.Department.DepartmentName,
                            Kontenjan = _company.Amount

                        }).ToList();
            return JsonConvert.SerializeObject(new { success = true, message = "Tebirkler", data = list });


        }


        [Route("API/selectDepertmentAdmin")]
        public int SelectDepertment(int companyId, int departmentId)
        {
            var amount = companyDepartmantManager.GetId(companyId, departmentId);

            return amount;
        }

        #endregion
    }
}
