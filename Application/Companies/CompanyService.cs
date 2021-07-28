using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Companies.Requests;
using Application.Companies.Responses;
using Application.RepositoryInterfaces;
using Domain;
using Domain.Companies;
using Domain.Companies.ValueObjects;

namespace Application.Companies
{
    public sealed class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IList<CompanyResponse>> GetAllCompaniesAsync()
        {
            var response = new List<CompanyResponse>();

            var companies = await _companyRepository.GetAllAsync();

            foreach (var company in companies)
            {
                var companyResponse = new CompanyResponse
                {
                    Id = company.Id,
                    CompanyName = company.CompanyName
                };

                response.Add(companyResponse);
            }

            return response;
        }

        public async Task<Result<CompanyResponse>> GetCompanyByAsync(Guid id)
        {
            var company = await _companyRepository.GetByIdAsync(id);

            if (company == null)
            {
                return Result.Failure<CompanyResponse>($"Company with Id {id} was not found");
            }

            var response = new CompanyResponse()
            {
                Id = company.Id,
                CompanyName = company.CompanyName
            };

            return Result.Success(response);
        }

        public async Task<Result> CreateCompanyAsync(CreateCompanyRequest request)
        {
            Result<CompanyName> companyNameOrError = CompanyName.Create(request?.CompanyName);

            if (companyNameOrError.IsFailure)
            {
                return Result.Failure(companyNameOrError.Error);
            }

            var company = new Company(companyNameOrError.Value);

            await _companyRepository.AddAsync(company);

            return Result.Success();
        }

        public async Task<Result> UpdateCompanyAsync(Guid companyId, UpdateCompanyRequest request)
        {
            Result<CompanyName> companyNameOrError = CompanyName.Create(request?.CompanyName);

            if (companyNameOrError.IsFailure)
            {
                return Result.Failure(companyNameOrError.Error);
            }

            var company = await _companyRepository.GetByIdAsync(companyId);

            if (company == null)
            {
                return Result.Failure($"Company with Id {companyId} was not found");
            }

            company.UpdateCompany(companyNameOrError.Value);

            await _companyRepository.Update(company);

            return Result.Success();
        }

        public async Task<Result> DeleteCompanyAsync(Guid companyId)
        {
            var company = await _companyRepository.GetByIdAsync(companyId);

            if (company == null)
            {
                return Result.Failure($"Company with Id {companyId} was not found");
            }

            await _companyRepository.Delete(company);

            return Result.Success();
        }
    }
}
