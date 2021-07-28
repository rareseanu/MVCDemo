using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Companies;
using Domain.Companies.ValueObjects;
using Domain.Employees;
using Domain.Employees.ValueObjects;
using Infrastructure.Context;

namespace Infrastructure.Migrations
{
    public static class DbInitializer
    {
        public static async Task Initialize(AppDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            var companyDbSet = context.Set<Company>();

            if (companyDbSet.Any())
            {
                return;
            }

            Company[] companies = { CreateCompanyWithEmployees() };

            await companyDbSet.AddRangeAsync(companies);

            await context.SaveChangesAsync();
        }

        private static Company CreateCompanyWithEmployees()
        {
            var companyName = CompanyName.Create("Company 1").Value;

            var company = new Company(companyName);

            company.Employees = new List<Employee>()
            {
                CreateEmployee1(company.Id),
                CreateEmployee2(company.Id)
            };

            return company;
        }

        private static Employee CreateEmployee1(Guid companyId)
        {
            var address = Address.Create("street", "city", "country").Value;
            var name = Name.Create("Employee 1 First Name", "Employee 1 last name").Value;

            var employee = new Employee(address, name, "employee1@company1.com", companyId);

            return employee;
        }

        private static Employee CreateEmployee2(Guid companyId)
        {
            var address = Address.Create("street", "city", "country").Value;
            var name = Name.Create("Employee 2 First Name", "Employee 2 last name").Value;

            var employee = new Employee(address, name, "employee2@company1.com", companyId);

            return employee;
        }
    }
}
