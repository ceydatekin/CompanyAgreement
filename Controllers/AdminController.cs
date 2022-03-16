using CompanyAgreement.Manager;
using CompanyAgreement.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace CompanyAgreement.Controllers
{
    public class AdminController : Controller
    {
        CompanyManager companyManager = new CompanyManager();   


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

                _ = companyManager.Insert(new Models.Company()
                {
                    CompanyName = model.CompanyName,
                    ContractSituation = model.contractsituation,
                    MeetingDate = model.MeetingDate,
                    PublicPrivate = model.PublicPrivate,
                    ContractInformation = model.ContractInformation,
                    CompanyInformation = model.CompanyInformation



                });
               
                return JsonConvert.SerializeObject(new { success = true, message = "Tebrikler" });

            }
        }

        public class addCompanyModel
        {
            public string CompanyName { get; set; }
            public DateTime MeetingDate { get; set; }
            public bool PublicPrivate { get; set; }
            public ContractSituation contractsituation { get; set; }
            public ContractInformation ContractInformation { get; set; } 
            public CompanyInformation CompanyInformation { get; set; } 
  
        }


    }
}
