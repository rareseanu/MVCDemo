using System;
using Domain.Base;
using Domain.Companies;
using Domain.Employees.ValueObjects;

namespace Domain.Employees
{
    public class Employee : BasicEntity
    {
        private Employee()
        {

        }

        public Employee(Address address, Name name, string email, Guid companyId)
        {
            Id = Guid.NewGuid();
            Address = address;
            Name = name;
            CompanyId = companyId;
            Email = email;
        }

        public Name Name { get; private set; }
        public string Email { get; set; }
        public Address Address { get; private set; }
        public Guid CompanyId { get; private set; }
        public Company Company { get; set; }

        public void UpdateEmployee(Address address, Name name, string email, Guid companyId)
        {
            Address = address;
            Name = name;
            CompanyId = companyId;
            Email = email;
        }

    }
}
