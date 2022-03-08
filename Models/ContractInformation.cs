using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyAgreement.Models
{
    public class ContractInformation
    {
        [Key]
        public int Id { get; set; }
        public string Mail { get; set; }
        public string GSM { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(300)]
        public string Address { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string Province { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string District { get; set; }
        public ICollection<Company> Companies { get; set; } 
    }
}
