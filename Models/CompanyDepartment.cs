using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyAgreement.Models
{
    public class CompanyDepartment
    {
       
        [Key]
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int Amount { get; set; }
    }
}
