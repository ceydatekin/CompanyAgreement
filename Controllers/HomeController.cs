using CompanyAgreement.Helper;
using CompanyAgreement.Manager;
using CompanyAgreement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAgreement.Controllers
{
    public class EmployeeDetailsController : ControllerBase
    {
        
 
}
    public class HomeController : Controller

    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        AdminLoginManager adminLoginManager = new AdminLoginManager();
        SessionHelper sessionHelper;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            sessionHelper = new SessionHelper(_httpContextAccessor);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AcademicianLogin()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CompanyLogin()
        {
            return View();
        }
        [HttpGet]

        public IActionResult OidbLogin()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
