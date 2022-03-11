using Microsoft.AspNetCore.Mvc;

namespace CompanyAgreement.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
