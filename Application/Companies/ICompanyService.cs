using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Companies.Requests;
using Application.Companies.Responses;
using Domain;

namespace Application.Companies
{
    public interface ICompanyService
    {
        Task<IList<CompanyResponse>> GetAllCompaniesAsync();
        Task<Result<CompanyResponse>> GetCompanyByAsync(Guid id);
        Task<Result> CreateCompanyAsync(CreateCompanyRequest request);
        Task<Result> UpdateCompanyAsync(Guid companyId, UpdateCompanyRequest request);
        Task<Result> DeleteCompanyAsync(Guid companyId);
    }
}