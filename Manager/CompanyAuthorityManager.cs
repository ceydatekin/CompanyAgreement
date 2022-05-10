using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAgreement.Models;
using CompanyAgreement.Repository;

namespace CompanyAgreement.Manager
{
    public class CompanyAuthorityManager : IRepository<CompanyAuthority>
    {
        Context contextManager = ContextManager.GetContext();
        public List<CompanyAuthority> AllCompanyAuthority() => this.contextManager.CompanyAuthorities.ToList();
        public CompanyAuthority GetId(int companyId) => contextManager.CompanyAuthorities.SingleOrDefault(s => s.Id == companyId);
    }
}
