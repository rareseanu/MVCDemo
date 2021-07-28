using Application.Employees.Requests;
using Application.Employees.Responses;
using Application.RepositoryInterfaces;
using Domain;
using Domain.Companies;
using Domain.Employees;
using Domain.Employees.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Employees
{
    class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompanyRepository _companyRepository;

        public EmployeeService(IEmployeeRepository employeeRepository,
            ICompanyRepository companyRepository)
        {
            _employeeRepository = employeeRepository;
            _companyRepository = companyRepository;
        }

        public async Task<Result> CreateEmployeeAsync(CreateEmployeeRequest request)
        {
            Result<Name> nameOrError = Name.Create(request?.FirstName, request?.LastName);

            if (nameOrError.IsFailure)
            {
                return Result.Failure(nameOrError.Error);
            }

            Result<Address> addressOrError = Address.Create(request?.Street, request?.City, request?.Country);

            if (addressOrError.IsFailure)
            {
                return Result.Failure(addressOrError.Error);
            }

            Company company = await _companyRepository.GetByIdAsync(request.CompanyId);

            if(company == null)
            {
                return Result.Failure("Company ID not found.");
            }

            var employee = new Employee(addressOrError.Value, nameOrError.Value, request?.Email, company.Id);

            await _employeeRepository.AddAsync(employee);

            return Result.Success();
        }

        public async Task<Result> DeleteEmployeeyAsync(Guid employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);

            if (employee == null)
            {
                return Result.Failure($"Employee with Id {employeeId} was not found");
            }

            await _employeeRepository.Delete(employee);

            return Result.Success();
        }

        public async Task<IList<EmployeeResponse>> GetAllEmployeesAsync()
        {
            var response = new List<EmployeeResponse>();

            var employees = await _employeeRepository.GetAllAsync();

            foreach (var employee in employees)
            {
                var employeeResponse = new EmployeeResponse
                {
                    Id = employee.Id,
                    FirstName = employee.Name.FirstName, 
                    LastName = employee.Name.LastName,
                    City = employee.Address.City,
                    Country = employee.Address.Country,
                    Street = employee.Address.Street,
                    Email = employee.Email,
                    CompanyId = employee.CompanyId
                };

                response.Add(employeeResponse);
            }

            return response;
        }

        public async Task<Result<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return Result.Failure<EmployeeResponse>($"Employee with Id {id} was not found");
            }

            var response = new EmployeeResponse()
            {
                Id = employee.Id,
                FirstName = employee.Name.FirstName,
                LastName = employee.Name.LastName,
                City = employee.Address.City,
                Country = employee.Address.Country,
                Street = employee.Address.Street,
                Email = employee.Email,
                CompanyId = employee.CompanyId
            };

            return Result.Success(response);
        }

        public async Task<Result> UpdateEmployeeAsync(Guid employeeId, UpdateEmployeeRequest request)
        {
            Result<Name> nameOrError = Name.Create(request?.FirstName, request?.LastName);

            if (nameOrError.IsFailure)
            {
                return Result.Failure(nameOrError.Error);
            }

            Result<Address> addressOrError = Address.Create(request?.Street, request?.City, request?.Country);

            if (addressOrError.IsFailure)
            {
                return Result.Failure(addressOrError.Error);
            }

            var employee = await _employeeRepository.GetByIdAsync(employeeId);

            if (employee == null)
            {
                return Result.Failure($"Employee with Id {employeeId} was not found");
            }

            Company company = await _companyRepository.GetByIdAsync(request.CompanyId);

            if (company == null)
            {
                return Result.Failure("Company ID not found.");
            }

            employee.UpdateEmployee(addressOrError.Value, nameOrError.Value, request?.Email, request.CompanyId);

            await _employeeRepository.Update(employee);

            return Result.Success();
        }
    }
}
