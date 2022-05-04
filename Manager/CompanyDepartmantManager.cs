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
        //public CompanyDepartment GetQuota(int CompanyId)
        //{

        //    return contextManager.CompanyDepartments.SingleOrDefault(s => s.CompanyId == CompanyId);
        //}

        public int GetId(int companyId, int departmentId)
        {
            var amount = contextManager.CompanyDepartments.SingleOrDefault(s => s.CompanyId == companyId && s.DepartmentId == departmentId);

            if (amount == null)
                return 0;

            return amount.Amount;
        }



        public void GetObject(int companyId, int departmentId, int amount)
        {
            var cDepartment = contextManager.CompanyDepartments.SingleOrDefault(s => s.CompanyId == companyId && s.DepartmentId == departmentId);
            cDepartment.Amount = amount;
        }



        //public void AddCompanyDepartment(string CompanyName, DateTime MeetingDate, bool PublicPrivate, int CompanyInformationId)
        //{
        //    Insert(new Models.CompanyDepartment()
        //    {
        //        CompanyName = CompanyName,
        //        MeetingDate = MeetingDate,
        //        PublicPrivate = PublicPrivate,
        //        CompanyInformationId = CompanyInformationId //companyInformationManager.GetById(model.CompanyInformation_Name, model.CompanyInformation_Surname, model.CompanyInformation_mail)


        //    });
        //}
    }
}

