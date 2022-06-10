using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAgreement.Models;
using CompanyAgreement.Repository;

namespace CompanyAgreement.Manager
{
    public class CompanyLoginManager : IRepository<CompanyLogin>
    {
        public CompanyLogin GetUser(string username, string password) => ContextManager.GetContext().CompanyLogin.SingleOrDefault(entity => entity.UserName == username && entity.Password == password);
    }
}
