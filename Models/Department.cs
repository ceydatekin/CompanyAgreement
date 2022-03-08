using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyAgreement.Models
{
    //Firmaların hangi bölümlerden öğrenci alacağı 
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string DepartmentName { get; set; }
        public ICollection<CompanyDepartment> CompanyDepartments { get; set; }       
             
        
    }
}
