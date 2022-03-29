using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyAgreement.Models
{
    //Hocaların ilk not aldığı bilgiler
    public class CompanyInformation
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName ="Varchar")]
        [StringLength(20)]
        public string Name { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string GSM { get; set; }

       // public int CompanyId { get; set; }
       // public Company Company { get; set; }    
}
}
