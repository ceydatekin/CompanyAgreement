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
        //  public IEnumerable<Company> Find(Expression<Func<Company, bool>> predicate) => this.contextManager.Companies.Where().ToList();
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
       //company id ye göre companynin sahip olduğu departmanların listelenmesi (modal kısmını açmadan önce listeleme sayfasında olması gereken veriler
       //örn diniz holding psikoloji kontenjan: 3)
        //public List<Company> DepartmentListForCompany(int companyId)
        //{

        //    //DepartmantManager departmantManager = new DepartmantManager();  
        //    //return departmantManager.AllDepartments().Include(companyId).ToList();   
        //}
        public Company GetId(int companyId) => contextManager.Companies.SingleOrDefault(s => s.Id == companyId);
     
        public List<Company> AllCompanies() => contextManager.Companies.ToList();




    }
}
