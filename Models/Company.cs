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
        public virtual CompanyInformation CompanyInformation { get; set; }
        //Contract situation 1-1
       // public virtual ContractSituation ContractSituation { get; set; }
        //Department n-n
       // public virtual ContractInformation ContractInformation { get; set; }
        public ICollection<CompanyDepartment> CompanyDepartments { get; set; }

    }
}
