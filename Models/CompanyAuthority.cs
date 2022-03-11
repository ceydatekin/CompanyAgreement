﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CompanyAgreement.Models
{
    //Resmi Firma Bilgileri
    public class CompanyAuthority
    {
        [Key]
        public int Id { get; set; }
        public string SGKNO { get; set; }
        public string TaxNumber { get; set; }
        public DateTime ContractDate { get; set; }
        public virtual ContractSituation ContractSituation { get; set; }
        public virtual CompanyLogin CompanyLogin { get; set; }
    }
}