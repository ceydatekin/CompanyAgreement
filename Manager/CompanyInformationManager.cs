﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAgreement.Models;
using CompanyAgreement.Repository;

namespace CompanyAgreement.Manager
{
    public class CompanyInformationManager : IRepository<CompanyInformation>
    {
        Context contextManager = ContextManager.GetContext();
        public List<CompanyInformation> AllCompanyInformation() => this.contextManager.CompanyInformation.ToList();
       public CompanyInformation GetById1(string name, string surname, string mail) => contextManager.CompanyInformation.SingleOrDefault(i=> i.Name == name && i.Surname == surname && i.Mail == mail);
        public void AddCompanyInformation(string CompanyInformation_mail,string CompanyInformation_GSM,string CompanyInformation_Name,string CompanyInformation_Surname)
        {
            Insert(new Models.CompanyInformation()
            {
                Mail = CompanyInformation_mail,
                
                GSM = CompanyInformation_GSM,
                Name = CompanyInformation_Name,
                Surname = CompanyInformation_Surname,
            });
        }
    }
}
