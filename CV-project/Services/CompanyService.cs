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
    public class CompanyService : ICompanyService
    {
        private readonly REOrganizationContext _context;

        public CompanyService(
            REOrganizationContext context)
        {
            _context = context;
        }
        public async Task<bool> RegisterAsync(RegisterCompanyViewModel registerViewModel)
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
                AccountType = 2,/*1-applicant, 2-company,3-admin*/
            };
            _context.Accounts.Add(newAccount);
            _context.SaveChanges();
            var idAccount = await (from f in _context.Accounts
                                   where f.Username == registerViewModel.UserName
                                   select f.AccountId).FirstOrDefaultAsync();
           
            var newCompany = new Company()
            {
                CompanyId = System.Guid.NewGuid(),
                AccountId = idAccount,
                Name = registerViewModel.FullName,
            };
            _context.Companies.Add(newCompany);
            _context.SaveChanges();
            return true;
        }
        //public async Task<bool> CreateCompanyProfile(Guid applicantId, ProfileViewModel profile)
        //{
        //    var id = (from c in _context.Applicants
        //              join d in _context.Accounts on c.AccountId equals d.AccountId
        //              where d.AccountId == applicantId
        //              select c.Cvid
        //              ).FirstOrDefault();
        //    var cvProfile = await (from c in _context.WebCvs
        //                           where c.Cvid == id
        //                           select c
        //                           ).FirstOrDefaultAsync();
        //    cvProfile.FullName = profile.Name;
        //    cvProfile.BriefIntroduction = profile.BiefIntroduction;
        //    cvProfile.CareerObjectives = profile.Careerobjecives;
        //    //cvProfile.C = profile.ContactInformation;
        //    cvProfile.Awards = profile.Awards;
        //    cvProfile.Certificates = profile.Certificates;
        //    cvProfile.WorkExperience = (short?)profile.WorkExperiences;
        //    cvProfile.Activities = profile.Activities;
        //    cvProfile.FullName = profile.Name;
        //    _context.SaveChanges();
        //    return true;
        //}
        public async Task<bool> CreateJobAsync(Guid idCompany, JobViewModel jobVM)
        {
            Guid idJob = System.Guid.NewGuid();
            var newJob = new Job()
            {
                JobId = idJob,
                Name = jobVM.Name,
                Vacancy = jobVM.Position,
            };
            _context.Jobs.Add(newJob);
            var idCom = (from c in _context.Companies
                      join d in _context.Accounts on c.AccountId equals d.AccountId
                      where d.AccountId == idCompany
                      select c.CompanyId
                      ).FirstOrDefault();
            var relationHasJob = new HaveJob()
            {
                CompanyId = idCom,
                JobId = idJob,
                Description = jobVM.Information,
                Deadline = jobVM.Deadline
            };
            _context.HaveJobs.Add(relationHasJob);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
