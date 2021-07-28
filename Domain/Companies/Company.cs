using System;
using System.Collections.Generic;
using Domain.Base;
using Domain.Companies.ValueObjects;
using Domain.Employees;

namespace Domain.Companies
{
    public sealed class Company : BasicEntity
    {
        public CompanyName CompanyName { get; private set; }
        public ICollection<Employee> Employees { get; set; }

        private Company()
        {

        }

        public Company(CompanyName companyName)
        {
            Id = Guid.NewGuid();
            CompanyName = companyName;
        }

        public void UpdateCompany(CompanyName companyName)
        {
            CompanyName = companyName;
        }
    }
}
