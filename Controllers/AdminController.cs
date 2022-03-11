using Microsoft.AspNetCore.Mvc;

namespace CompanyAgreement.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
