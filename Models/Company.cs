using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyAgreement.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public bool PublicPrivate { get; set; }
        public DateTime MeetingDate { get; set; }
        //Company information 1-1
     
        //Contract situation 1-1
        public  ContractSituation ContractSituation { get; set; }
        public ICollection<ContractInformation> ContractInformations { get; set; }

        //Department n-n
        public ICollection<CompanyDepartment> CompanyDepartments { get; set; }
        public int CompanyInformationId { get; set; }
        public CompanyInformation CompanyInformation { get; set; }

        //public virtual CompanyLogin CompanyLogin { get; set; }

    }
}
