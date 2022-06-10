using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CompanyAgreement.Models;
using CompanyAgreement.Repository;
using Microsoft.EntityFrameworkCore;

namespace CompanyAgreement.Manager
{
    public class CompanyManager : IRepository<Company>
    {

        Context contextManager = ContextManager.GetContext();
        public Company GetById1(string CompanyName, DateTime MeetingDate) => contextManager.Companies.SingleOrDefault(i => i.CompanyName == CompanyName && i.MeetingDate == MeetingDate);
        public void AddCompany(string CompanyName, DateTime MeetingDate, bool PublicPrivate, int CompanyInformationId)
        {
            Insert(new Models.Company()
            {
                CompanyName = CompanyName,
                MeetingDate = MeetingDate,
                PublicPrivate = PublicPrivate,
                CompanyInformationId = CompanyInformationId //companyInformationManager.GetById(model.CompanyInformation_Name, model.CompanyInformation_Surname, model.CompanyInformation_mail)


            });
        }

        public Company GetId(int companyId) => contextManager.Companies.FirstOrDefault(s => s.Id == companyId);
     
        public List<Company> AllCompanies() => contextManager.Companies.ToList();



        //public void GetObject(int companyId, int departmentId, int amount)
        //{
        //    var cDepartment = contextManager.CompanyDepartments.SingleOrDefault(s => s.CompanyId == companyId && s.DepartmentId == departmentId);
        //    cDepartment.Amount = amount;
        //    cDepartment.DepartmentId = departmentId;
        //    cDepartment.CompanyId = companyId;
        //    Delete(cDepartment);
        //    Update(cDepartment);
        //}




    }
}
