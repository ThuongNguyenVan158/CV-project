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
    }
}
