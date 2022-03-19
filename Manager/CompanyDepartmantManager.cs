﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAgreement.Models;
using CompanyAgreement.Repository;

namespace CompanyAgreement.Manager
{
    public class CompanyDepartmantManager : IRepository<CompanyDepartment>
    {
        Context contextManager = ContextManager.GetContext();
        public List<CompanyDepartment> AllCompaniesDepartment() => this.contextManager.CompanyDepartments.ToList();
    }
}
