using CompanyAgreement.Models;
using System.Collections.Generic;

namespace CompanyAgreement.modelview
{
    public class CompanyListViewModel
    {
        public List<Company> Company { get; set; }
        public List<CompanyDepartment> CompanyDepartment { get; set; }
        public List<CompanyInformation> CompanyInformation { get; set; }
        public List<CompanyAuthority> CompanyAuthority { get; set; }
        public List<ContractSituation> ContractSituation { get; set; }
        public List<ContractInformation> ContractInformation { get; set; }

    }
}
