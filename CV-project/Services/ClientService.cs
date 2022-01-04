using CV_project.Data;
using CV_project.Data.Entities;
using CV_project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagedList;
using PagedList.Mvc;

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
                var birthdate = await (from c in _context.Accounts
                                       join d in _context.Applicants on c.AccountId equals d.AccountId
                                       where c.Username == loginViewModel.UserName && c.Password == loginViewModel.Password
                                       select d.DoB
                                       ).FirstOrDefaultAsync();
                if (birthdate == null)
                    infoSession.IscreateProfile = 0; //0 mean have no cv, 1 mean have one
                else infoSession.IscreateProfile = 1;
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
                var desc = await (from c in _context.Accounts
                                  join d in _context.Companies on c.AccountId equals d.AccountId
                                  where c.Username == loginViewModel.UserName && c.Password == loginViewModel.Password
                                  select d.Description
                                  ).FirstOrDefaultAsync();
                if (desc == null)
                    infoSession.IscreateProfile = 0;
                else infoSession.IscreateProfile = 1;
                }
            return infoSession;
        }
        public async Task<bool> CreateCV(Guid applicantId, CVViewModel cvVM)
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
            cvProfile.FullName = cvVM.Name;
            cvProfile.DoB = cvVM.DoB;
            cvProfile.Major = cvVM.Major;
            cvProfile.University = cvVM.University;
            cvProfile.Skills = cvVM.Skills;
            cvProfile.BriefIntroduction = cvVM.BiefIntroduction;
            cvProfile.CareerObjectives = cvVM.Careerobjecives;
            cvProfile.Awards = cvVM.Awards;
            cvProfile.Certificates = cvVM.Certificates;
            cvProfile.WorkExperience = (short?)cvVM.WorkExperiences;
            cvProfile.Activities = cvVM.Activities;
            _context.SaveChanges();
            return true;
        }
        public async Task<List<CompanyViewModel>> PagingCompany()
        {
            var listCompany = await (from c in _context.Companies
                                     orderby c.Name
                                     select new CompanyViewModel
                                     {
                                         Name = c.Name,
                                         Description = c.Description,
                                         Address = c.Address,
                                         NoEmployee = c.NoEmployee,
                                         CompanyId = c.CompanyId
                                     }).ToListAsync();
            return listCompany;
        }
        public async Task<List<JobPerCompany>> GetJobPerCompany(Guid idCompany)
        {
            var listJob = new List<JobPerCompany>();
            listJob = await (from c in _context.HaveJobs
                             join d in _context.Jobs on c.JobId equals d.JobId
                             where c.CompanyId == idCompany
                             select new JobPerCompany
                             {
                                 Name = d.Name,
                                 Vacancy = d.Vacancy,
                                 Description = c.Description,
                                 Deadline = c.Deadline,
                                 Status = c.Status

                             }).ToListAsync();
            return listJob;
        }
        public async Task<CVViewModel> GetCV(Guid applicantId)
        {
            var objCV = await (from c in _context.Applicants
                               join d in _context.WebCvs on c.Cvid equals d.Cvid
                               where c.AccountId == applicantId
                               select new CVViewModel()
                               {
                                   Name = d.FullName,
                                   DoB= (DateTime)d.DoB,
                                   BiefIntroduction=d.BriefIntroduction,
                                   Careerobjecives = d.CareerObjectives,
                                   Certificates =d.Certificates,
                                   Activities=d.Activities,
                                   Awards=d.Awards,
                                   Skills=d.Skills,
                                   Major=d.Major,
                                   University=d.University,
                                   WorkExperiences= (int)d.WorkExperience
                               }).FirstOrDefaultAsync();
            return objCV;
        }
        public async Task<bool> CreateProfile(Guid applicantId, ProfileViewModel cvVM)
        {
            var profile = await(from c in _context.Applicants
                      join d in _context.Accounts on c.AccountId equals d.AccountId
                      where d.AccountId == applicantId
                      select c
                      ).FirstOrDefaultAsync();
            profile.FullName = cvVM.Name;
            profile.DoB = cvVM.DoB;
            //profile.Major = cvVM.Major;
            profile.University = cvVM.University;
            //profile.Skills = cvVM.Skills;
            profile.BriefIntroduction = cvVM.BiefIntroduction;
            //profile.CareerObjectives = cvVM.Careerobjecives;
            //profile.Awards = cvVM.Awards;
            //profile.Certificates = cvVM.Certificates;
            //profile.WorkExperiences = (short?)cvVM.WorkExperiences;
            //profile.Activities = cvVM.Activities;
            profile.Contact = cvVM.ContactInformation;
            profile.EmailAddress = cvVM.Email;
            _context.SaveChanges();
            return true;
        }
        public async Task<ProfileViewModel> GetProfile(Guid applicantId)
        {
            var objCV = await (from c in _context.Applicants
                               join d in _context.Accounts on c.AccountId equals d.AccountId
                               where c.AccountId == applicantId
                               select new ProfileViewModel()
                               {
                                   Name = c.FullName,
                                   DoB = (DateTime)c.DoB,
                                   BiefIntroduction = c.BriefIntroduction,
                                   //Careerobjecives = d.CareerObjectives,
                                   //Certificates = d.Certificates,
                                   //Activities = d.Activities,
                                   //Awards = d.Awards,
                                   //Skills = d.Skills,
                                   //Major = d.Major,
                                   ContactInformation = c.Contact,
                                   University = c.University,
                                   Email = c.EmailAddress
                                   //WorkExperiences = (int)d.WorkExperience
                               }).FirstOrDefaultAsync();
            return objCV;
        }
    }
}
