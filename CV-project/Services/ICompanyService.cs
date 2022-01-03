using CV_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_project.Services
{
    public interface ICompanyService
    {
        Task<bool> RegisterAsync(RegisterCompanyViewModel registerViewModel);
        Task<bool> CreateJobAsync(Guid idCompany,JobViewModel jobVM);
    }
}
