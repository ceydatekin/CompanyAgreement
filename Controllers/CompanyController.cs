using CompanyAgreement.Manager;
using CompanyAgreement.modelview;
using Microsoft.AspNetCore.Mvc;
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
        #endregion
        #region Firma Ekleme
        [HttpGet]
        public IActionResult Index()
        {
            var session = 1;
            var companyViewmodel = new CompanyViewModel();
            companyViewmodel.Company = companyManager.GetId(session);
            companyViewmodel.CompanyDepartment = companyDepartmantManager.AllCompaniesDepartment().ToList();
            companyViewmodel.CompanyInformation = companyInformationManager.GetCompyIdInformation(session);
            companyViewmodel.CompanyAuthority = companyAuthorityManager.GetId(session);
            companyViewmodel.ContractSituation = cantractSituationManager.GetId(session);
            companyViewmodel.ContractInformation = contractInformationManager.GetId(session);
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
                companyAuthorityManager.addCompanyAuthority(model.CompanyAuthority_SGKNO, model.CompanyAuthority_TaxNumber, model.CompanyAuthority_ContractDate,1);
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
    }
}
