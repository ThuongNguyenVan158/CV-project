using CV_project.Data.Entities;
using CV_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_project.Services
{
    public interface ICompanyService
    {
        //Task<bool> RegisterAsync(RegisterCompanyViewModel registerViewModel);
        Task<bool> CreateJobAsync(Guid idCompany,JobViewModel jobVM);
        Task<bool> CreaCompanyProfile(Guid accountId, CompanyProfile comProfile);
        Task<CompanyProfile> GetCompanyProfile(Guid accountId);
        Task<List<JobViewModel>> GetAllJobPerCompany(Guid accountId);
        Task<bool> UpdateJob(Guid accountId, JobViewModel jobVM);
        Task<JobViewModel> GetJob(Guid accountId, Guid jobId);
        Task<List<WebCv>> GetListCvPerCompany(Guid accountId);
    }
}
