using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyAgreement.Models
{
    public class CompanyLogin
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string UserName { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string Password { get; set; }
        public int Companyid { get; set; }
       // public Company Company { get; set; }
    }
}
