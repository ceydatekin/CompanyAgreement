using CompanyAgreement.Models;
using System.Collections.Generic;

namespace CompanyAgreement.modelview
{
    public class CompanyViewModel
    {
        public Company Company { get; set; }
        public List<CompanyDepartment> CompanyDepartment { get; set; }
        public CompanyInformation CompanyInformation { get; set; }
        public CompanyAuthority CompanyAuthority { get; set; }
        public ContractSituation ContractSituation { get; set; }
        public ContractInformation ContractInformation { get; set; }
         
    }
}
