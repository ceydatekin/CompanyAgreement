using CompanyAgreement.Models;
using System.Collections.Generic;

namespace CompanyAgreement.modelview
{
    public class CompanyQuotaViewModel
    {
        public int DepartmentId { get; set; }
        public int Amount { get; set; }
        public List<Department> Departments { get; set; }
        public List<CompanyDepartment> CompanyDepartment { get; set; }
    }
}
