using CV_project.Models;
using CV_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PagedList;
using PagedList.Mvc;
using CV_project.Data;
using CV_project.Data.Entities;

namespace CV_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClientService _clientService;
        private readonly REOrganizationContext _context;

        public HomeController(ILogger<HomeController> logger, IClientService clientService, REOrganizationContext context)
        {
            _logger = logger;
            _clientService = clientService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> ViewCompany(int pageNumber=1)
        {
            var model = new PagingJobCompany();
            model.listJobPerCompany = new List<JobPerCompany>();
            model.company = await PagingList<Company>.CreareAsync(_context.Companies, pageNumber, 1);
            model.listJobPerCompany = await _clientService.GetJobPerCompany(model.company.FirstOrDefault().CompanyId);
            return View(model);
        }
        public IActionResult ViewJob()
        {
            return View();
        }
        public IActionResult ViewEvent()
        {
            return View();
        }

        [HttpGet("/SignIn")]
        public IActionResult SignIn()
        {
            return View(new LoginViewModel());
        }
        [HttpPost("/SignIn")]
        public async Task<IActionResult> SignIn(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            int accountType = await _clientService.LoginAsync(loginViewModel);
            if(accountType > 0)
            {
               var sessionInfo = await _clientService.GetInfoSession(loginViewModel);
                // set value into session key
                HttpContext.Session.SetString("Usersession", JsonConvert.SerializeObject(sessionInfo));
            }    
            if (accountType == 0)
                return View(loginViewModel);
            if (accountType == 1)
                return RedirectToAction("ViewCompany");
            return RedirectToAction("ViewCV", "Company");
        }
        [HttpGet("/CreateCV")]
        public IActionResult CreateCV()
        {
            return View(new ProfileViewModel());
        }
        [HttpPost("/CreateCV")]
        public async Task<IActionResult> CreateCV(ProfileViewModel profile)
        {
            if (!ModelState.IsValid)
            {
                return View(profile);
            }
            if (HttpContext.Session.GetString("Usersession") == null)
                return RedirectToAction("SignIn");

            InfoViewModel infoSession = new InfoViewModel();
            infoSession = JsonConvert.DeserializeObject<InfoViewModel>(HttpContext.Session.GetString("Usersession"));
            await _clientService.CreateProfile(infoSession.accountId, profile);
            return View();
        }
        [HttpGet("/SignUp")]
        public IActionResult SignUp()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost("/SignUp")]
        public async Task<IActionResult> SignUp(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }
            var registerSucess = await _clientService.RegisterAsync(registerViewModel);

            if (!registerSucess)
            {
                return View(registerViewModel);
            }
            return RedirectToAction("SignIn");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Usersession");
            return RedirectToAction("SignIn");
        }
        public IActionResult Headhunt()
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
