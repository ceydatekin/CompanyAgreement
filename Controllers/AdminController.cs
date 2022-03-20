using CompanyAgreement.Manager;
using CompanyAgreement.Models;
using CompanyAgreement.modelview;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace CompanyAgreement.Controllers
{
    public class AdminController : Controller
    {
        CompanyManager companyManager = new CompanyManager();
        CantractSituationManager cantractSituationManager = new CantractSituationManager();
        ContractInformationManager contractInformationManager = new ContractInformationManager();
        CompanyInformationManager companyInformationManager = new CompanyInformationManager();
        CompanyDepartmantManager companyDepartmantManager = new CompanyDepartmantManager();
        CompanyAuthorityManager companyAuthorityManager = new CompanyAuthorityManager();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListCompany()
        {
            var companyListViewModel = new CompanyListViewModel();
            companyListViewModel.Company = companyManager.AllCompanies();
            companyListViewModel.CompanyDepartment = companyDepartmantManager.AllCompaniesDepartment();
            companyListViewModel.CompanyInformation = companyInformationManager.AllCompanyInformation();
            companyListViewModel.CompanyAuthority = companyAuthorityManager.AllCompanyAuthority();
            companyListViewModel.ContractSituation = cantractSituationManager.AllCantractSituation();
        
            return View(companyListViewModel);  

      
        }

        //Firma Giriş Sayfasındaki Form veri tabanına ekleme API'si
        [HttpPost]
        [Route("API/AddCompany")]
        public string addCompany([FromForm] addCompanyModel model)
        {



            {

                companyManager.Insert(new Models.Company()
                {
                    CompanyName = model.CompanyName,
                    MeetingDate = model.MeetingDate,
                    PublicPrivate = model.PublicPrivate,
                });
                cantractSituationManager.Insert(new Models.ContractSituation()
                {
                    Situation = model.Situations,
                    Description = model.Description
                });
                companyInformationManager.Insert(new Models.CompanyInformation()
                {
                    Mail = model.CompanyInformation_mail,
                     
                    GSM = model.CompanyInformation_GSM,
                    Name = model.CompanyInformation_Name,
                    Surname = model.CompanyInformation_Surname,
                });
                contractInformationManager.Insert(new Models.ContractInformation(){
                     Mail = model.ContractInformation_Mail,
                     GSM= model.ContractInformation_Gsm,
                     Address = model.ContractInformation_Adress,
                     Province = model.ContractInformation_Province,
                     District= model.ContractInformation_District,
                        
                });
                    

                return JsonConvert.SerializeObject(new { success = true, message = "Tebrikler" });

            }
        }

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


    }
}
