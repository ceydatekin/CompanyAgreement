using CompanyAgreement.Models;
using CompanyAgreement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAgreement.Manager
{
    public class DepartmantManager : IRepository<Department>
    {
        Context contextManager = ContextManager.GetContext();
        public Department GetId(int departmantId) => contextManager.Departments.SingleOrDefault(s => s.Id == departmantId);
        public int FindDepartmentNameId(string departmantName) => contextManager.Departments.SingleOrDefault(s => s.DepartmentName == departmantName).Id;
        public List<Department> AllDepartments() => this.contextManager.Departments.ToList();

    }
}
