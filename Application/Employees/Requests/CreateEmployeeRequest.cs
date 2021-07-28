using Domain.Employees.ValueObjects;
using System;

namespace Application.Employees.Requests
{
    public class CreateEmployeeRequest
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid CompanyId { get; set; }
    }
}
