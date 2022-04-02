﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAgreement.Models;
using CompanyAgreement.Repository;

namespace CompanyAgreement.Manager
{
    public class ContractInformationManager : IRepository<ContractInformation>
    {

        Context contextManager = ContextManager.GetContext();
        public List<ContractInformation> AllContractInformation() => this.contextManager.ContractInformation.ToList();

        public void addContractInformation(string mail, string Gsm, string adress, string province, string district )
        {
          Insert(new Models.ContractInformation()
            {
                Mail = mail,
                GSM = Gsm,
                Address = adress,
                Province = province,
                District = district,
            });
        }

       
    }
}
