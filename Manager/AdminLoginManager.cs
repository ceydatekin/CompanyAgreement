using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAgreement.Models;
using CompanyAgreement.Repository;


namespace CompanyAgreement.Manager
{
    public class AdminLoginManager : IRepository<AdminLogin>
    {
        public AdminLogin GetUser(string username, string password) =>ContextManager.GetContext().AdminLogins.FirstOrDefault(entity => entity.UserName == username && entity.Password == password);
    }

}
