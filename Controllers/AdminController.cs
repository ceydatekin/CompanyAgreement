using CompanyAgreement.Helper;
using CompanyAgreement.Manager;
using CompanyAgreement.Models;
using CompanyAgreement.modelview;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json;
using System;
using System.Linq;

using System.Net;
//using System.Net.Mail;

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
        AcademicianLoginManager academicianLoginManager = new AcademicianLoginManager();

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
                            companyId = _company.Id,
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

                return JsonConvert.SerializeObject(new { success = true, message = "Tebirkler", data = filterListPublicPrivate });
            }
            if (model.PublicPrivate != "space" && model.Situations != "space")
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
            if (model.Location != "space" && model.Situations != "space")
            {
                bool PublicPrivate = model.PublicPrivate == "Özel" ? true : false;
                var company = companyManager.GetAll();
                var filterListPublicPrivate = (from _company in company
                                               where _company.ContractInformations.SingleOrDefault(x => x.Company == _company).Province == model.Location
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

            if (model.Location != "space" && model.Situations != "space" && model.PublicPrivate != "space")
            {
                bool PublicPrivate = model.PublicPrivate == "Özel" ? true : false;
                var company = companyManager.GetAll();
                var filterListPublicPrivate = (from _company in company
                                               where _company.PublicPrivate == PublicPrivate
                                               where _company.ContractInformations.SingleOrDefault(x => x.Company == _company).Province == model.Location
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
                            Kontenjan = _company.Amount,
                            departmantID = _company.DepartmentId,

                        }).ToList();
            return JsonConvert.SerializeObject(new { success = true, message = "Tebirkler", data = list });
        }


        [Route("API/selectDepertmentAdmin")]
        public int SelectDepertment(int companyId, int departmentId)
        {
            var amount = companyDepartmantManager.GetId(companyId, departmentId);

            return amount;
        }


        [HttpPost]
        [Route("API/openModalAdmin")]
        public string openModal(string getDepartmant, int ID, int companyId)
        {
            var companies = companyDepartmantManager.GetAllDepartment(companyId);
            var list = (from _company in companies
                        where _company.DepartmentId == ID
                        select new
                        {
                            CompanyId = _company.CompanyId,
                            DepartmentName = _company.Department.DepartmentName,
                            Kontenjan = _company.Amount,
                            departmantID = _company.DepartmentId,

                        }).ToList();

            if (getDepartmant == "OK")
            {

                return JsonConvert.SerializeObject(new { status = true, message = "Veri başarayıla getirildi.", data = list });

            }
            else
            {
                return JsonConvert.SerializeObject(new { status = false, message = "Bilinmeyen istek." });
            }


        }

        [HttpPost]
        [Route("API/UpdateQuotaAdmin")]
        public string updateQuota([FromForm] updateQuotaModel model)
        {
            try
            {
                companyDepartmantManager.Insert(new Models.CompanyDepartment()
                {
                    CompanyId = model.CompanyId,
                    DepartmentId = departmantManager.FindDepartmentNameId(model.ModalDepartmentName),
                    Amount = model.ModalAmount,
                });
            }
            catch (Exception)
            {
                companyDepartmantManager.GetObject(model.CompanyId, departmantManager.FindDepartmentNameId(model.ModalDepartmentName), model.ModalAmount);
                //companyDepartmantManager.GetObject()
            }

            return JsonConvert.SerializeObject(new { success = true, message = "Tebrikler" });

        }

        public class updateQuotaModel
        {
            public int CompanyId { get; set; }
            public string ModalDepartmentName { get; set; }
            public int ModalAmount { get; set; }


        }
        #endregion

        #region Akademisyen Şifre Atama

        public IActionResult AssignmentAcademician()
        {
            //if (String.IsNullOrEmpty(sessionHelper.Get("UserAdminName")))
            //{
            //    return RedirectToAction("OidbLogin");
            //}
            var addQuotaViewModel = new AddQuotaViewModel();
            addQuotaViewModel.CompanyDepartment = companyDepartmantManager.AllCompaniesDepartment().ToList();
            addQuotaViewModel.Companies = companyManager.AllCompanies().ToList();
            addQuotaViewModel.Departments = departmantManager.AllDepartments().ToList();
            return View(addQuotaViewModel);

        }

        [HttpPost]
        [Route("API/mailSend")]
        public string sendMAil([FromForm] AcademicianInformation model)
        {

            academicianLoginManager.AddAcademician(model.UserName, model.Name, model.Surname, model.DepartmentId);
            return JsonConvert.SerializeObject(new { success = true, message = "Tebrikler" });

            //// create email message
            //var email = new MimeMessage();
            //email.From.Add(MailboxAddress.Parse("imep.btu@gmail.com"));
            //email.To.Add(MailboxAddress.Parse("ceydatekn85@gmail.com"));
            //email.Subject = "Test Email Subject";
            //email.Body = new TextPart(TextFormat.Html) { Text = "<h1>" + "Deneme Maildir" + "</h1>" };


            //using (var client = new SmtpClient())
            //{
            //    //client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

            //    //client.AuthenticationMechanisms.Remove("XOAUTH2");
            //    //client.Authenticate("imep.btu@gmail.com", "sepimep2022");
            //    //client.Send(email);

            //    //client.Disconnect(true);
            //}

        }
        public class AcademicianInformation
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public int DepartmentId { get; set; }
            public string UserName { get; set; }
        }

        #endregion

        #region 

        public IActionResult DefineAcademician()
        {
            return View();
        }

        #endregion

        #region listeleme Düzenleme
        [HttpPost]
        [Route("API/openModalCompay")]
        public string openModalCompanyUpdate(string getCompany, int ID)
        {
            var companies = companyManager.AllCompanies();
            var list = (from _company in companies
                            //join _companyAuthority in contextManager.CompanyAuthorities on _company.Id equals _companyAuthority.CompanyId
                        where _company.Id == ID
                        select new
                        {
                            CompanyId = _company.Id,
                            CompanyInformation_Name = _company.CompanyInformation.Name,
                            CompanyInformation_Surname = _company.CompanyInformation.Surname,
                            CompanyInformation_mail = _company.CompanyInformation.Mail,
                            CompanyInformation_GSM = _company.CompanyInformation.GSM,
                            //CompanyAuthority_SGKNO = _companyAuthority.SGKNO,
                            //CompanyAuthority_TaxNumber = _companyAuthority.TaxNumber,
                            //CompanyAuthority_ContractDate = _companyAuthority.ContractDate,
                            CompanyName = _company.CompanyName,
                            Description = _company.ContractSituation.Description,
                            ContractInformation_Mail = _company.ContractInformations.SingleOrDefault(x => x.Company == _company).Mail,
                            ContractInformation_GSM = _company.ContractInformations.SingleOrDefault(x => x.Company == _company).GSM,
                            ContractInformation_Adress = _company.ContractInformations.SingleOrDefault(x => x.Company == _company).Address,
                            ContractInformation_Province = _company.ContractInformations.SingleOrDefault(x => x.Company == _company).Province,
                            ContractInformation_District = _company.ContractInformations.SingleOrDefault(x => x.Company == _company).District,
                            PublicPrivate = _company.PublicPrivate ? "Özel" : "Kamu",
                            Situation = _company.ContractSituation.Situation,
                            MeetingDate = _company.MeetingDate,
                            NameSurname = _company.CompanyInformation.Name + " " + _company.CompanyInformation.Surname,
                            location = _company.ContractInformations.SingleOrDefault(x => x.Company == _company).Province + " / " + _company.ContractInformations.SingleOrDefault(x => x.Company == _company).District,

                        }).ToList();

            if (getCompany == "OK")
            {

                return JsonConvert.SerializeObject(new { status = true, message = "Veri başarayıla getirildi.", data = list });

            }
            else
            {
                return JsonConvert.SerializeObject(new { status = false, message = "Bilinmeyen istek." });
            }


        }

        [HttpPost]
        [Route("API/UpdateCompany")]
        public string updateCompany([FromForm] updateCompanyModel model)
        {
   
            cantractSituationManager.Delete(contextManager.ContractSituation.SingleOrDefault(item => item.CompanyId == model.CompanyId));
            contractInformationManager.Delete(contextManager.ContractInformation.SingleOrDefault(item => item.CompanyId == model.CompanyId));
            companyInformationManager.Delete(contextManager.CompanyInformation.SingleOrDefault(item => item.Company == contextManager.Companies.SingleOrDefault(item => item.Id == model.CompanyId)));


            companyInformationManager.AddCompanyInformation(model.CompanyInformation_mail, model.CompanyInformation_GSM, model.CompanyInformation_Name, model.CompanyInformation_Surname);
            int CompanyInformationId = companyInformationManager.GetById1(model.CompanyInformation_Name, model.CompanyInformation_Surname, model.CompanyInformation_mail).Id;
            companyManager.AddCompany(model.CompanyName, model.MeetingDate, model.PublicPrivate, CompanyInformationId);

            int CompanyId = companyManager.GetById1(model.CompanyName, model.MeetingDate).Id;
            contractInformationManager.addContractInformation(model.ContractInformation_Mail, model.ContractInformation_Gsm, model.ContractInformation_Adress, model.ContractInformation_Province, model.ContractInformation_District, CompanyId);
            cantractSituationManager.AddContractSituation(model.Situation, model.Description, CompanyId);

            return JsonConvert.SerializeObject(new { success = true, message = "Tebrikler" });



        }
        //Sayfadan gelecek olan firma bilgileri formdata
        public class updateCompanyModel
        {
            public string CompanyName { get; set; }//yapıldı
            public int CompanyId { get; set; }//yapıldı
            public DateTime MeetingDate { get; set; }//yapıldı
            public bool PublicPrivate { get; set; }//yapıldı
            public string Situation { get; set; }//yapıldı
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



    }
}

