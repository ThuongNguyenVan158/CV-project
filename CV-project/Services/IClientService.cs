using CV_project.Data.Entities;
using CV_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CV_project.Services
{
    public interface IClientService
    {
        Task<int> LoginAsync(LoginViewModel loginViewModel);
        Task<bool> RegisterAsync(RegisterViewModel registerViewModel);
        Task<InfoViewModel> GetInfoSession(LoginViewModel loginViewModel);
        Task<List<CompanyViewModel>> PagingCompany();
        Task<List<JobPerCompany>> GetJobPerCompany(Guid idCompany);
        Task<bool> CreateCV(Guid applicantId, CVViewModel cvVM);
        Task<CVViewModel> GetCV(Guid applicantId);
        Task<bool> CreateProfile(Guid applicantId, ProfileViewModel cvVM);
        Task<ProfileViewModel> GetProfile(Guid applicantId);
    }
}
