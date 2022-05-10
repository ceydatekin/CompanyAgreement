﻿using CompanyAgreement.Manager;
using CompanyAgreement.Models;
using CompanyAgreement.modelview;
using Microsoft.AspNetCore.Mvc;
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
        #endregion

        #region Firma Ekleme
        [HttpGet]
        public IActionResult Index()
        {
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
            var companyDepartmentList = companyDepartmantManager.GetAcademicianCompany(1).ToList();

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
    }
}