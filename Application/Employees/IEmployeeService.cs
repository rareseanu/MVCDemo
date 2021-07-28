using Application.Employees.Requests;
using Application.Employees.Responses;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Employees
{
    public interface IEmployeeService
    {
        Task<IList<EmployeeResponse>> GetAllEmployeesAsync();
        Task<Result<EmployeeResponse>> GetEmployeeByIdAsync(Guid id);
        Task<Result> CreateEmployeeAsync(CreateEmployeeRequest request);
        Task<Result> UpdateEmployeeAsync(Guid employeeId, UpdateEmployeeRequest request);
        Task<Result> DeleteEmployeeyAsync(Guid employeeId);
    }
}
