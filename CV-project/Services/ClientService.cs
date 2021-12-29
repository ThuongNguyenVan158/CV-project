using CV_project.Data;
using CV_project.Data.Entities;
using CV_project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_project.Services
{
    public class ClientService : IClientService
    {
        private readonly REOrganizationContext _context;

        public ClientService(
            REOrganizationContext context)
        {
            _context = context;
        }
        public async Task<int> LoginAsync(LoginViewModel loginViewModel)
        {
            var customer = await (from c in _context.Accounts
                                  where c.Username == loginViewModel.UserName && c.Password == loginViewModel.Password
                                  select c).FirstOrDefaultAsync();
            if (customer == null)
            {
                return 0;
            }
            return customer.AccountType;
        }
        public async Task<bool> RegisterAsync(RegisterViewModel registerViewModel)
        {
            //if (registerViewModel.Password != registerViewModel.RePassword) return false;

            var customer = await (from c in _context.Accounts
                                  where c.Username == registerViewModel.UserName
                                  select c).FirstOrDefaultAsync();
            if (customer != null)
            {
                return false;
            }
            var newAccount = new Account()
            {
                AccountId = System.Guid.NewGuid(),
                Username = registerViewModel.UserName,
                Password = registerViewModel.Password,
                AccountType = 1,/*1-applicant, 2-company,3-admin*/
            };
            _context.Accounts.Add(newAccount);
            _context.SaveChanges();
            var idAccount = await (from f in _context.Accounts
                                   where f.Username == registerViewModel.UserName
                                   select f.AccountId).FirstOrDefaultAsync();
            Guid idCV = System.Guid.NewGuid();
            var newCV = new WebCv()
            {
                Cvid = idCV,
                FullName = registerViewModel.FullName
            };
            _context.WebCvs.Add(newCV);
            var newApplicant = new Applicant()
            {
                ApplicantId = System.Guid.NewGuid(),
                AccountId = idAccount,
                FullName = registerViewModel.FullName,
                EmailAddress = registerViewModel.Email,
                Cvid = idCV
            };
            _context.Applicants.Add(newApplicant);
            _context.SaveChanges();
            return true;
        }
        public async Task<InfoViewModel> GetInfoSession(LoginViewModel loginViewModel)
        {
            var account = await (from c in _context.Accounts
                                  where c.Username == loginViewModel.UserName && c.Password == loginViewModel.Password
                                  select c).FirstOrDefaultAsync();
            if (account == null)
            {
                return new InfoViewModel();
            }
            var infoSession = new InfoViewModel();
            if (account.AccountType == 1)
                {
                infoSession = await (from c in _context.Accounts
                                     join d in _context.Applicants on c.AccountId equals d.AccountId
                                     where c.Username == loginViewModel.UserName && c.Password == loginViewModel.Password
                                     select new InfoViewModel()
                                     {
                                         accountId = c.AccountId,
                                         AccountType = c.AccountType,
                                         FullName = d.FullName
                                     }).FirstOrDefaultAsync();
                }
            else if (account.AccountType == 2)
                {
                infoSession = await (from c in _context.Accounts
                                     join d in _context.Companies on c.AccountId equals d.AccountId
                                     where c.Username == loginViewModel.UserName && c.Password == loginViewModel.Password
                                     select new InfoViewModel()
                                     {
                                         accountId = c.AccountId,
                                         AccountType = c.AccountType,
                                         FullName = d.Name
                                     }).FirstOrDefaultAsync();
                }
            return infoSession;
        }
        public async Task<bool> CreateProfile(Guid applicantId,ProfileViewModel profile)
        {
            var id = (from c in _context.Applicants
                      join d in _context.Accounts on c.AccountId equals d.AccountId
                      where d.AccountId == applicantId
                      select c.Cvid
                      ).FirstOrDefault();
            var cvProfile = await (from c in _context.WebCvs
                                   where c.Cvid == id
                                   select c
                                   ).FirstOrDefaultAsync();
            cvProfile.FullName = profile.Name;
            cvProfile.BriefIntroduction = profile.BiefIntroduction;
            cvProfile.CareerObjectives = profile.Careerobjecives;
            //cvProfile.C = profile.ContactInformation;
            cvProfile.Awards = profile.Awards;
            cvProfile.Certificates = profile.Certificates;
            cvProfile.WorkExperience = (short?)profile.WorkExperiences;
            cvProfile.Activities = profile.Activities;
            cvProfile.FullName = profile.Name;
            _context.SaveChanges();
            return true;
        }
    }
}
