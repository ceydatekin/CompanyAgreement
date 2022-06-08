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
    public class AdminController : Controller
    {
        #region Manager
        Context contextManager = ContextManager.GetContext();
        CompanyManager companyManager = new CompanyManager();
        CantractSituationManager cantractSituationManager = new CantractSituationManager();
        ContractInformationManager contractInformationManager = new ContractInformationManager();
        CompanyInformationManager companyInformationManager = new CompanyInformationManager();
        CompanyDepartmantManager companyDepartmantManager = new CompanyDepartmantManager();
        CompanyAuthorityManager companyAuthorityManager = new CompanyAuthorityManager();
        DepartmantManager departmantManager = new DepartmantManager();

        private readonly ILogger<AdminController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        AdminLoginManager adminLoginManager = new AdminLoginManager();
        SessionHelper sessionHelper;

        #endregion

        #region Controller 
        public AdminController(ILogger<AdminController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            sessionHelper = new SessionHelper(_httpContextAccessor);

        }

        #endregion

        #region Login kontrolü session yapısı
        public IActionResult OidbLogin()
        {
            return View();
        }

        [Route("API/AdminLogin")]
        [HttpPost]
        public bool AdminLogin([FromForm] AdminUserModel model)
        {

            var user = adminLoginManager.GetUser(model.userName, model.password);
            if (user == null)
            {
                return false;
            }
            else
            {
                sessionHelper.Set("UserAdminName", model.userName);
                sessionHelper.Set("UserAdminId", user.Id);
                sessionHelper.Set("FiilterCompanyNumber", 0);
                return true;

            }
        }
        public class AdminUserModel
        {
            public string userName { get; set; }
            public string password { get; set; }
        }

        [Route("API/AdminLogout")]
        [HttpPost]
        public bool logout()
        {
            sessionHelper.Set("UserAdminName", "");
            sessionHelper.Set("UserAdminId", 0);
            return true;
            //  return View("OidbLogin");

        }
        #endregion

        #region Firma Listele

        public IActionResult ListCompany()
        {
            if (String.IsNullOrEmpty(sessionHelper.Get("UserAdminName")))
            {
                return RedirectToAction("OidbLogin");
            }
            if ((int)sessionHelper.Getid("FiilterCompanyNumber") == 0)
            {
                var companyListViewModel = new CompanyListViewModel();
                companyListViewModel.Company = companyManager.AllCompanies().ToList();
                companyListViewModel.CompanyDepartment = companyDepartmantManager.AllCompaniesDepartment().ToList();
                companyListViewModel.CompanyInformation = companyInformationManager.AllCompanyInformation().ToList();
                companyListViewModel.CompanyAuthority = companyAuthorityManager.AllCompanyAuthority().ToList();
                companyListViewModel.ContractSituation = cantractSituationManager.AllCantractSituation().ToList();
                companyListViewModel.ContractInformation = contractInformationManager.AllContractInformation().ToList();
                return View(companyListViewModel);
            }
            return View();
        }
        // join _ogrencisistemi in context.OgrenciSistemis on _ogrenci.Id equals _ogrencisistemi.Ogrenci
        [HttpGet]
        [Route("API/ListAdminCompany")]
        public string listAdminCompany()
        {
            var companies = companyManager.GetAll();
            var list = (from _company in companies
                        select new
                        {
                            CompanyName = _company.CompanyName,
                            PublicPrivate = _company.PublicPrivate ? "Özel" : "Kamu",
                            Situation = _company.ContractSituation.Situation,
                            MeetingDate = _company.MeetingDate,
                            NameSurname = _company.CompanyInformation.Name + " " + _company.CompanyInformation.Surname,
                            location = _company.ContractInformations.SingleOrDefault(x => x.Company == _company).Province + " / " + _company.ContractInformations.SingleOrDefault(x => x.Company == _company).District,
                        }).ToList();
            return JsonConvert.SerializeObject(new { success = true, message = "Tebirkler", data = list });
        }


        [HttpPost]
        [Route("API/FilterCompany")]
        public string FilterCompany([FromForm] FilterModel model)
        {
            
            if (model.Situations == "space" && model.Location == "space" && model.PublicPrivate == "space")
                return null;

            if (model.Situations != "space")
            {
                var company = companyManager.GetAll();
                var filterListSituations = (from _company in company
                            where _company.ContractSituation.Situation == model.Situations
                            select new
                            {
                                CompanyName = _company.CompanyName,
                                PublicPrivate = _company.PublicPrivate ? "Özel" : "Kamu",
                                Situation = _company.ContractSituation.Situation,
                                MeetingDate = _company.MeetingDate,
                                NameSurname = _company.CompanyInformation.Name + " " + _company.CompanyInformation.Surname,
                                location = _company.ContractInformations.SingleOrDefault(x => x.Company == _company).Province + " / " + _company.ContractInformations.SingleOrDefault(x => x.Company == _company).District,
                            }).ToList();

                return JsonConvert.SerializeObject(new { success = true, message = "Tebirkler", data = filterListSituations });
            }
            if (model.Location != "space")
            {
                var company = companyManager.GetAll();
                var filterListLocation = (from _company in company
                                            where _company.ContractInformations.SingleOrDefault(x => x.Company == _company).Province == model.Location
                                            select new
                                            {
                                                CompanyName = _company.CompanyName,
                                                PublicPrivate = _company.PublicPrivate ? "Özel" : "Kamu",
                                                Situation = _company.ContractSituation.Situation,
                                                MeetingDate = _company.MeetingDate,
                                                NameSurname = _company.CompanyInformation.Name + " " + _company.CompanyInformation.Surname,
                                                location = _company.ContractInformations.SingleOrDefault(x => x.Company == _company).Province + " / " + _company.ContractInformations.SingleOrDefault(x => x.Company == _company).District,
                                            }).ToList();

                return JsonConvert.SerializeObject(new { success = true, message = "Tebirkler", data = filterListLocation });
            }
            if (model.PublicPrivate != "space")
            {
                bool PublicPrivate = model.PublicPrivate == "Özel" ? true : false;
                var company = companyManager.GetAll();
                var filterListPublicPrivate = (from _company in company
                                            where _company.PublicPrivate == PublicPrivate
                                               select new
                                            {
                                                CompanyName = _company.CompanyName,
                                                PublicPrivate = _company.PublicPrivate ? "Özel" : "Kamu",
                                                Situation = _company.ContractSituation.Situation,
                                                MeetingDate = _company.MeetingDate,
                                                NameSurname = _company.CompanyInformation.Name + " " + _company.CompanyInformation.Surname,
                                                location = _company.ContractInformations.SingleOrDefault(x => x.Company == _company).Province + " / " + _company.ContractInformations.SingleOrDefault(x => x.Company == _company).District,
                                            }).ToList();

                return JsonConvert.SerializeObject(new { success = true, message = "Tebirkler", data = filterListPublicPrivate });
            }

            if (model.PublicPrivate != "space" && model.Location != "space")
            {
                bool PublicPrivate = model.PublicPrivate == "Özel" ? true : false;
                var company = companyManager.GetAll();
                var filterListPublicPrivate = (from _company in company
                                               where _company.PublicPrivate == PublicPrivate
                                               where _company.ContractSituation.Situation == model.Situations
                                               select new
                                               {
                                                   CompanyName = _company.CompanyName,
                                                   PublicPrivate = _company.PublicPrivate ? "Özel" : "Kamu",
                                                   Situation = _company.ContractSituation.Situation,
                                                   MeetingDate = _company.MeetingDate,
                                                   NameSurname = _company.CompanyInformation.Name + " " + _company.CompanyInformation.Surname,
                                                   location = _company.ContractInformations.SingleOrDefault(x => x.Company == _company).Province + " / " + _company.ContractInformations.SingleOrDefault(x => x.Company == _company).District,
                                               }).ToList();

                return JsonConvert.SerializeObject(new { success = true, message = "Tebirkler", data = filterListPublicPrivate });
            }

            var companies = companyManager.GetAll();
            var list = (from _company in companies
                        select new
                        {
                            CompanyName = _company.CompanyName,
                            PublicPrivate = _company.PublicPrivate ? "Özel" : "Kamu",
                            Situation = _company.ContractSituation.Situation,
                            MeetingDate = _company.MeetingDate,
                            NameSurname = _company.CompanyInformation.Name + " " + _company.CompanyInformation.Surname,
                            location = _company.ContractInformations.SingleOrDefault(x => x.Company == _company).Province + " / " + _company.ContractInformations.SingleOrDefault(x => x.Company == _company).District,
                        }).ToList();
            return JsonConvert.SerializeObject(new { success = true, message = "Tebirkler", data = list });

        }

        public class FilterModel
        {
            public string Situations { get; set; }
            public string Location { get; set; }
            public string PublicPrivate { get; set; }
        }

        #endregion

        #region Firma Ekleme
        [HttpGet]
        public IActionResult Index()
        {
            if (String.IsNullOrEmpty(sessionHelper.Get("UserAdminName")))
            {
                return RedirectToAction("OidbLogin");
            }
            return View();
        }

        //Firma Giriş Sayfasındaki Form veri tabanına ekleme API'si
        [HttpPost]
        [Route("API/AddCompanyAdmin")]
        public string addCompany([FromForm] addCompanyModel model)
        {
            {
                companyInformationManager.AddCompanyInformation(model.CompanyInformation_mail, model.CompanyInformation_GSM, model.CompanyInformation_Name, model.CompanyInformation_Surname);
                int CompanyInformationId = companyInformationManager.GetById1(model.CompanyInformation_Name, model.CompanyInformation_Surname, model.CompanyInformation_mail).Id;
                companyManager.AddCompany(model.CompanyName, model.MeetingDate, model.PublicPrivate, CompanyInformationId);

                int CompanyId = companyManager.GetById1(model.CompanyName, model.MeetingDate).Id;
                contractInformationManager.addContractInformation(model.ContractInformation_Mail, model.ContractInformation_Gsm, model.ContractInformation_Adress, model.ContractInformation_Province, model.ContractInformation_District, CompanyId);
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

        #region Firma Kontenjan Ekleme
        //Firma Kontenjan Ekleme
        [HttpPost]
        [Route("API/AddQuotaAdmin")]

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
            if (String.IsNullOrEmpty(sessionHelper.Get("UserAdminName")))
            {
                return RedirectToAction("OidbLogin");
            }
            var addQuotaViewModel = new AddQuotaViewModel();
            addQuotaViewModel.CompanyDepartment = companyDepartmantManager.AllCompaniesDepartment().ToList();
            addQuotaViewModel.Companies = companyManager.AllCompanies().ToList();
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
        [Route("API/quotaListAdmin")]
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


        [Route("API/selectDepertmentAdmin")]
        public int SelectDepertment(int companyId, int departmentId)
        {
            var amount = companyDepartmantManager.GetId(companyId, departmentId);

            return amount;
        }

        #endregion

        #region 

        public IActionResult DefineAcademician()
        {
            return View();
        }

        #endregion
    }
}

