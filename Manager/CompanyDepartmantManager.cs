using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAgreement.Models;
using CompanyAgreement.Repository;

namespace CompanyAgreement.Manager
{
    public class CompanyDepartmantManager : IRepository<CompanyDepartment>
    {
        Context contextManager = ContextManager.GetContext();
        public List<CompanyDepartment> AllCompaniesDepartment() => this.contextManager.CompanyDepartments.ToList();
        public CompanyDepartment GetQuota(int CompanyId)
        {
           
            return contextManager.CompanyDepartments.SingleOrDefault(s => s.CompanyId == CompanyId);
        }
        public void UpdateQuota(int CompanyId)
        {
            CompanyDepartment companyDepartment = new CompanyDepartment();
            companyDepartment = GetQuota(CompanyId);
            // companyDepartment.DepartmentId == 1;

            switch (companyDepartment.DepartmentId)
            {
                case 1: 
                default:
                    break;
            }
        }

    }
}
