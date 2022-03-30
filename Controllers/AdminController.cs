﻿using CompanyAgreement.Manager;
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
        DepartmantManager departmantManager = new DepartmantManager();

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
                int CompanyInformationId = 10; //companyInformationManager.GetById(model.CompanyInformation_Name, model.CompanyInformation_Surname, model.CompanyInformation_mail)
                companyInformationManager.AddCompanyInformation(model.CompanyInformation_mail, model.CompanyInformation_GSM, model.CompanyInformation_Name, model.CompanyInformation_Surname);

                companyManager.AddCompany(model.CompanyName, model.MeetingDate, model.PublicPrivate, CompanyInformationId);
                contractInformationManager.addContractInformation(model.ContractInformation_Mail, model.ContractInformation_Gsm, model.ContractInformation_Adress, model.ContractInformation_Province, model.ContractInformation_District);

                cantractSituationManager.AddContractSituation(model.Situations, model.Description);

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

        //Firma Kontenjan Ekleme
        [HttpPost]
        [Route("API/AddQuota")]
        public string addQuota([FromForm] addQuotaModel model)
        {

            {

                companyDepartmantManager.Insert(new Models.CompanyDepartment()
                {
                    Company = companyManager.GetId(model.CompanyId),
                    Department = departmantManager.GetId(model.DepartmentId),
                    CompanyId = model.CompanyId,
                    DepartmentId = model.DepartmentId,
                    Amount = model.Amount,

                });
                return JsonConvert.SerializeObject(new { success = true, message = "Tebrikler" });

            }
        }

        public class addQuotaModel
        {
            public int CompanyId { get; set; }
            public int DepartmentId { get; set; }
            public int Amount { get; set; }


        }
    }
}
