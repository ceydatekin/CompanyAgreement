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
        public CompanyAuthority GetId(int companyId)
        {
            var item = contextManager.CompanyAuthorities.SingleOrDefault(s => s.Id == companyId);
            if (item == null)
                return null;
            return item;
        }

        public void addCompanyAuthority(string SGKno, string TaxNumber, DateTime ContractDate, int session)
        {
            Insert(new Models.CompanyAuthority()
            {
                SGKNO = SGKno,
                TaxNumber = TaxNumber,
                ContractDate = ContractDate,
                CompanyId = session,
            });
        }
    }
}
