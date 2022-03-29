using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyAgreement.Models
{
    //EXCELDEKİ K1,K2,K3 KODLARI
    public class ContractSituation
    {
        [Key]
        public int Id { get; set; }
        public string Situation{ get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Description{ get; set; }
       // public int CompanyId { get; set; }
       // public Company Company { get; set; }
       // public int CompanyAuthorityId { get; set; }
       // public CompanyAuthority CompanyAuthority { get; set; }
    }
}

