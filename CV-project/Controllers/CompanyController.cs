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
        [HttpGet("/CreateCompanyProfile")]
        public IActionResult CreateCompanyProfile()
        {
            return View();
        }
        [HttpPost("/CreateCompanyProfile")]
        public async Task<IActionResult> CreateCompanyProfile(CompanyProfile comProfile)
        {
            if (!ModelState.IsValid)
            {
                return View(comProfile);
            }
            if (HttpContext.Session.GetString("Usersession") == null)
                return RedirectToAction("SignIn");

            InfoViewModel infoSession = new InfoViewModel();
            infoSession = JsonConvert.DeserializeObject<InfoViewModel>(HttpContext.Session.GetString("Usersession"));
            await _companyService.CreaCompanyProfile(infoSession.accountId,comProfile);
            // reset session
            var newSession = new InfoViewModel()
            {
                accountId = infoSession.accountId,
                AccountType = infoSession.AccountType,
                FullName = infoSession.FullName,
                IscreateProfile = 1
            };
            HttpContext.Session.Remove("Usersession");
            HttpContext.Session.SetString("Usersession", JsonConvert.SerializeObject(newSession));
            return View();
        }
        [HttpGet("/UpdateCompanyProfile")]
        public async Task<IActionResult> UpdateCompanyProfile()
        {
            if (HttpContext.Session.GetString("Usersession") == null)
                return RedirectToAction("SignIn","Home");

            InfoViewModel infoSession = new InfoViewModel();
            infoSession = JsonConvert.DeserializeObject<InfoViewModel>(HttpContext.Session.GetString("Usersession"));
            var modelProfile = await _companyService.GetCompanyProfile(infoSession.accountId);
            return View(modelProfile);
        }
        [HttpPost("/UpdateCompanyProfile")]
        public async Task<IActionResult> UpdateCompanyProfile(CompanyProfile comProfile)
        {
            if (HttpContext.Session.GetString("Usersession") == null)
                return RedirectToAction("SignIn");

            InfoViewModel infoSession = new InfoViewModel();
            infoSession = JsonConvert.DeserializeObject<InfoViewModel>(HttpContext.Session.GetString("Usersession"));
            await _companyService.CreaCompanyProfile(infoSession.accountId, comProfile);
            return RedirectToAction("UpdateCompanyProfile");
        }
        [HttpGet("/CreateJob")]
        public IActionResult CreateJob()
        {
            return View(new JobViewModel());
        }
        [HttpPost("/CreateJob")]
        public async Task<IActionResult> CreateJob(JobViewModel jobVM)  
        {
            if (HttpContext.Session.GetString("Usersession") == null)
                return RedirectToAction("SignIn");

            InfoViewModel infoSession = new InfoViewModel();
            infoSession = JsonConvert.DeserializeObject<InfoViewModel>(HttpContext.Session.GetString("Usersession"));
            await _companyService.CreateJobAsync(infoSession.accountId, jobVM);
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
