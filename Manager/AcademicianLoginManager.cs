using CompanyAgreement.Models;
using CompanyAgreement.Repository;
using System.Linq;

namespace CompanyAgreement.Manager
{
    public class AcademicianLoginManager : IRepository<AcademicianLogin>
    {
        public AcademicianLogin GetUser(string username, string password) => ContextManager.GetContext().AcademicianLogins.SingleOrDefault(entity => entity.UserName == username && entity.Password == password );
    }
}
