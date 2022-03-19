using System;
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
    }
}
