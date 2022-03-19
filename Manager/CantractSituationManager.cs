using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAgreement.Models;
using CompanyAgreement.Repository; 

namespace CompanyAgreement.Manager
{
    public class CantractSituationManager : IRepository<ContractSituation>
    {
        Context contextManager = ContextManager.GetContext();
        public List<ContractSituation> AllCantractSituation() => this.contextManager.ContractSituation.ToList();
    }
}
