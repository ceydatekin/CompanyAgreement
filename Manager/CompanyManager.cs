using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CompanyAgreement.Models;
using CompanyAgreement.Repository;

namespace CompanyAgreement.Manager
{
    public class CompanyManager : IRepository<Company>
    {

        Context contextManager = ContextManager.GetContext();
        public IEnumerable<Company> Find(Expression<Func<Company, bool>> predicate) =>this.contextManager.Companies.Where(e => e.ContractSituation.Situation == "olumlu").ToList();
      
    }
}
