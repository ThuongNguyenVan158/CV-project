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
        public async Task<bool> CreaCompanyProfile(Guid accountId, CompanyProfile comProfile)
        {
            var profile = await (from c in _context.Companies
                                 join d in _context.Accounts on c.AccountId equals d.AccountId
                                 where d.AccountId == accountId
                                 select c
                     ).FirstOrDefaultAsync();
            profile.Name = comProfile.Name;
            profile.NoEmployee = (short?)comProfile.NoEmployee;
            profile.Description = comProfile.Description;
            profile.Address = comProfile.Address;
            _context.SaveChanges();
            return true;
        }
        public async Task<CompanyProfile> GetCompanyProfile(Guid accountId)
        {
            var objCV = await (from c in _context.Companies
                               join d in _context.Accounts on c.AccountId equals d.AccountId
                               where c.AccountId == accountId
                               select new CompanyProfile()
                               {
                                   Name = c.Name,
                                   NoEmployee = (int)c.NoEmployee,
                                   Address = c.Address,
                                   Description = c.Description
                               }).FirstOrDefaultAsync();
            return objCV;
        }
        public async Task<List<JobViewModel>> GetAllJobPerCompany(Guid accountId)
        {
            var idCompany = await (from c in _context.Accounts
                                   join d in _context.Companies on c.AccountId equals d.AccountId
                                   where c.AccountId == accountId
                                   select d.CompanyId
                                    ).FirstOrDefaultAsync();
            var listJob = new List<JobViewModel>();
            listJob = await (from c in _context.HaveJobs
                             join d in _context.Jobs on c.JobId equals d.JobId
                             where c.CompanyId == idCompany
                             select new JobViewModel
                             {
                                 JobId = c.JobId,
                                 Name = d.Name,
                                 Position = d.Vacancy,
                                 Information = c.Description,
                                 Deadline = c.Deadline,
                             }).ToListAsync();
            return listJob;
        }
        public async Task<JobViewModel> GetJob(Guid accountId, Guid jobId)
        {
            var idCompany = await (from c in _context.Accounts
                                   join d in _context.Companies on c.AccountId equals d.AccountId
                                   where c.AccountId == accountId
                                   select d.CompanyId
                                   ).FirstOrDefaultAsync();
            var job = await (from c in _context.Jobs
                             where c.JobId == jobId
                             select c).FirstOrDefaultAsync();
            var hasJob = await (from c in _context.HaveJobs
                                where c.CompanyId == idCompany && c.JobId == jobId
                                select c).FirstOrDefaultAsync();
            var jobInfo = new JobViewModel()
            {
                JobId = job.JobId,
                Name = job.Name,
                Position = job.Vacancy,
                Information = hasJob.Description,
                Deadline = hasJob.Deadline
            };
            return jobInfo;
        }    
        public async Task<bool> UpdateJob(Guid accountId, JobViewModel jobVM)
        {
            var idCompany = await (from c in _context.Accounts
                                   join d in _context.Companies on c.AccountId equals d.AccountId
                                   where c.AccountId == accountId
                                   select d.CompanyId
                                   ).FirstOrDefaultAsync();
            var job = await (from c in _context.Jobs
                             where c.JobId == jobVM.JobId
                             select c).FirstOrDefaultAsync();
            job.Name = jobVM.Name;
            job.Vacancy = jobVM.Position;
            _context.SaveChanges();
            var hasJob = await (from c in _context.HaveJobs
                                where c.CompanyId == idCompany && c.JobId == jobVM.JobId
                                select c).FirstOrDefaultAsync();
            hasJob.Description = jobVM.Information;
            hasJob.Deadline = jobVM.Deadline;
            _context.SaveChanges();
            return true;
        }
    }
}
