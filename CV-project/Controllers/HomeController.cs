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

namespace CV_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClientService _clientService;
        private readonly ICompanyService _companyService;

        public HomeController(ILogger<HomeController> logger, IClientService clientService, ICompanyService companyService)
        {
            _logger = logger;
            _clientService = clientService;
            _companyService = companyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ViewCompany()
        {
            return View();
        }
        public IActionResult ViewJob()
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
            var isSusccess = await _clientService.CreateProfile(infoSession.accountId, profile);
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
