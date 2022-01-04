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
    public class AdminService:IAdminService
    {
        private readonly REOrganizationContext _context;

        public AdminService(REOrganizationContext context)
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
    }
}
