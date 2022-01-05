using CV_project.Models;
using CV_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_project.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly IAdminService _adminService;
        public AdminController(ILogger<CompanyController> logger, IAdminService adminService)
        {
            _logger = logger;
            _adminService = adminService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("/RegisterForCompany")]
        public IActionResult RegisterForCompany()
        {
            if (HttpContext.Session.GetString("Usersession") == null)
                return RedirectToAction("SignIn","Home");
            InfoViewModel infoSession = new InfoViewModel();
            infoSession = JsonConvert.DeserializeObject<InfoViewModel>(HttpContext.Session.GetString("Usersession"));
            if(infoSession.AccountType!=3)
                return RedirectToAction("SignIn", "Home");
            return View(new RegisterCompanyViewModel());
        }
        [HttpPost("/RegisterForCompany")]
        public async Task<IActionResult> RegisterForCompany(RegisterCompanyViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }
            var registerSucess = await _adminService.RegisterAsync(registerViewModel);

            if (!registerSucess)
            {
                return View(registerViewModel);
            }
            return RedirectToAction("SignIn", "Home");
        }
        public IActionResult ListCompany()
        {
            if (HttpContext.Session.GetString("Usersession") == null)
                return RedirectToAction("SignIn", "Home");
            return View();
        }
    }
}
