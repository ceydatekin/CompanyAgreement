using CompanyAgreement.Manager;
using CompanyAgreement.Models;
using CompanyAgreement.modelview;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace CompanyAgreement.Controllers
{
    public class AdminController : Controller
    {
        #region Manager
        CompanyManager companyManager = new CompanyManager();
        CantractSituationManager cantractSituationManager = new CantractSituationManager();
        ContractInformationManager contractInformationManager = new ContractInformationManager();
        CompanyInformationManager companyInformationManager = new CompanyInformationManager();
        CompanyDepartmantManager companyDepartmantManager = new CompanyDepartmantManager();
        CompanyAuthorityManager companyAuthorityManager = new CompanyAuthorityManager();
        DepartmantManager departmantManager = new DepartmantManager();
        #endregion
        public IActionResult Login()
        {
            return View();
        }

        #region Firma Listele

        public IActionResult ListCompany()
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
        #endregion

        #region Firma Ekleme
        public IActionResult Index()
        {
            return View();
        }

        //Firma Giriş Sayfasındaki Form veri tabanına ekleme API'si
        [HttpPost]
        [Route("API/AddCompany")]
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

        #region Firma Kontenjan Ekleme
        //Firma Kontenjan Ekleme
        [HttpPost]
        [Route("API/AddQuota")]

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


        [Route("API/selectDepertment")]
        public int SelectDepertment(int companyId, int departmentId)
        {
            var amount = companyDepartmantManager.GetId(companyId, departmentId);

            return amount;
        }

        #endregion
    }
}
