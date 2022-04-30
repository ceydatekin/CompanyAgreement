using CompanyAgreement.Models;
using System.Collections.Generic;

namespace CompanyAgreement.modelview
{
    public class AddQuotaViewModel
    {
        public List<Department> Departments { get; set; }
        public List<CompanyDepartment> CompanyDepartment { get; set; }
        public List<Company> Companies { get; set; }
    }
}
