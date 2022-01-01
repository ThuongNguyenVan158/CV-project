using CV_project.Models;
using CV_project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_project.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyService _companyService;
        public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService)
        {
            _logger = logger;
            _companyService = companyService;
        }
        public IActionResult ViewCV()
        {
            return View();
        }
        [HttpGet("/Register")]
        public IActionResult RegisterForCompany()
        {
            return View(new RegisterCompanyViewModel());
        }
        [HttpPost("/Register")]
        public async Task<IActionResult> RegisterForCompany(RegisterCompanyViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }
            var registerSucess = await _companyService.RegisterAsync(registerViewModel);

            if (!registerSucess)
            {
                return View(registerViewModel);
            }
            return RedirectToAction("SignIn");
        }
        public IActionResult CreateCompanyProfile()
        {
            return View();
        }
        [HttpGet("/CreateJob")]
        public IActionResult CreateJob()
        {
            return View(new JobViewModel());
        }
        [HttpPost("/CreateJob")]
        public IActionResult CreateJob(JobViewModel jobVM)  
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
